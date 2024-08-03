using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StealAllTheCats.Infrastructure.Context;
using StealAllTheCats.Infrastructure.Context.Entities;
using StealAllTheCats.Infrastructure.Interfaces;

namespace StealAllTheCats.Infrastructure.Repositories;

public class CatsRepository(DBContext context, ILogger<CatsRepository> logger) : Repository<Cat>(context), ICatsRepository
{
    public async Task<bool> CreateAsync(Dictionary<Cat, List<Tag>> Entities)
    {
        using var transaction = context.Database.BeginTransaction();

        try
        {
            var existingCatIds = await context.Cats.Select(c => c.CatID).ToListAsync();
            var existingTags = await context.Tags.ToDictionaryAsync(t => t.Name, t => t);

            var newCats = Entities.Keys.Where(cat => !existingCatIds.Contains(cat.CatID)).ToList();
            var newTags = Entities.SelectMany(e => e.Value)
                                  .Where(tag => !existingTags.ContainsKey(tag.Name))
                                  .DistinctBy(tag => tag.Name)
                                  .ToList();

            if (newTags.Any())
            {
                context.Tags.AddRange(newTags);
                await context.SaveChangesAsync();

                foreach (var tag in newTags)
                    existingTags[tag.Name] = tag;
            }

            foreach (var cat in newCats)
            {
                var catTags = Entities[cat];
                cat.Tags = catTags.Select(tag => existingTags[tag.Name]).ToList();
            }

            context.Cats.AddRange(newCats);
            await context.SaveChangesAsync();

            await transaction.CommitAsync();
            return true;
        }
        catch (Exception exception)
        {
            logger.LogError($"EXCEPTION: {exception.Message} INNER EXCEPTION: {exception.InnerException}");
            transaction.Rollback();

            return false;
        }
    }

    public async Task<Cat?> GetByIDAsync(int ID)
    {
        return await context.Cats.Include(e => e.Tags)
                                 .FirstOrDefaultAsync(e => e.ID == ID);
    }

    public IQueryable<Cat> GetAll(string? TagName)
    {
        if(string.IsNullOrEmpty(TagName))
            return context.Cats.Include(e => e.Tags);
        else
            return context.Cats.Include(e => e.Tags)
                               .Where(e => e.Tags.Any(x => x.Name == TagName));
    }
}
