using AutoMapper;
using Microsoft.Extensions.Configuration;
using StealAllTheCats.Domain.DTOs.CatsApi;
using StealAllTheCats.Domain.DTOs.Entities;
using StealAllTheCats.Domain.Interfaces;
using StealAllTheCats.Domain.Shared.Constants;
using StealAllTheCats.Domain.Shared.Enums;
using StealAllTheCats.Domain.Shared.Helpers;
using StealAllTheCats.Infrastructure.Context.Entities;
using StealAllTheCats.Infrastructure.Interfaces;
using StealAllTheCats.Infrastructure.Shared.Paging;

namespace StealAllTheCats.Domain.Services;

public class CatsService(IConfiguration configuration, ICatsRepository repository, IMapper mapper, HttpHelper httpHelper) : ICatsService
{
    public async Task<(bool Succeeded, ErrorCodes ErrorCode)> FetchAsync()
    {
        Random rnd = new Random(); //random to retrieve different cats each time

        var requestUrl = string.Format(configuration.GetValue<string>("CatAPI:GetCatImages")!, Constants.DEFAULTNUMBEROFCATSFETCHED, rnd.Next(1, 10));
        var response = await httpHelper.GetAsync<List<CatAPI>>(requestUrl);

        var catsAndTags = new Dictionary<Cat, List<Tag>>();

        foreach (var cat in mapper.Map<List<Cat>>(response))
        {
            var unmappedCat = response.First(e => e.ID == cat.CatID);

            List<Tag> tags = new List<Tag>();
            foreach (var breed in unmappedCat.Breeds.Select(e => e.temperament))
                tags.AddRange(mapper.Map<List<Tag>>(breed.Replace(", ", ",").Split(",")));

            catsAndTags.Add(cat, tags);
        }

        var result = await repository.CreateAsync(catsAndTags);
        return (true, ErrorCodes.Default);
    }

    public async Task<CatDTO?> GetByIDAsync(int ID)
    {
        Cat? cat = await repository.GetByIDAsync(ID);

        if (cat != null)
            return mapper.Map<CatDTO>(cat);
        else
            return null;
    }

    public async Task<PagedList<CatDTO>> GetAllAsync(int PageNumber, int PageSize, string? TagName)
    {
        var query = repository.GetAll(TagName);
        var data = await repository.GetAllPagedAsync(query, PageNumber, PageSize);
        var cats = mapper.Map<PagedList<CatDTO>>(data);

        return cats;
    }
}
