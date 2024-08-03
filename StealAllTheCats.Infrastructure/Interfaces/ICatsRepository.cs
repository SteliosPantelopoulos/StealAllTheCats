using StealAllTheCats.Infrastructure.Context.Entities;

namespace StealAllTheCats.Infrastructure.Interfaces;

public interface ICatsRepository : IRepository<Cat>
{
    Task<bool> CreateAsync(Dictionary<Cat, List<Tag>> Entities);
    Task<Cat?> GetByIDAsync(int ID);
    IQueryable<Cat> GetAll(string? TagName);
}
