using StealAllTheCats.Domain.DTOs.Entities;
using StealAllTheCats.Domain.Shared.Enums;
using StealAllTheCats.Infrastructure.Shared.Paging;

namespace StealAllTheCats.Domain.Interfaces;

public interface ICatsService
{
    Task<(bool Succeeded, ErrorCodes ErrorCode)> FetchAsync();
    Task<CatDTO?> GetByIDAsync(int ID);
    Task<PagedList<CatDTO>> GetAllAsync(int PageNumber, int PageSize, string? TagName);
}
