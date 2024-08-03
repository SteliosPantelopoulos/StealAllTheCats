using StealAllTheCats.Infrastructure.Context;
using StealAllTheCats.Infrastructure.Context.Entities;
using StealAllTheCats.Infrastructure.Interfaces;
using StealAllTheCats.Infrastructure.Shared.Paging;

namespace StealAllTheCats.Infrastructure.Repositories;

public abstract class Repository<E> : IRepository<E> where E : BaseEntity
{
    private DBContext context { get; set; }

    protected Repository(DBContext context)
    {
        this.context = context;
    }

    public virtual async Task<PagedList<E>> GetAllPagedAsync(IQueryable<E> Query, int PageNumber, int PageSize)
        => await PagedList<E>.ToPagedListAsync(Query, PageNumber, PageSize);
}
