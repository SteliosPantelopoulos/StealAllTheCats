using StealAllTheCats.Infrastructure.Context.Entities;
using StealAllTheCats.Infrastructure.Shared.Paging;

namespace StealAllTheCats.Infrastructure.Interfaces;

public interface IRepository<E> where E : BaseEntity
{
    Task<PagedList<E>> GetAllPagedAsync(IQueryable<E> Query, int PageNumber, int PageSize);
}
