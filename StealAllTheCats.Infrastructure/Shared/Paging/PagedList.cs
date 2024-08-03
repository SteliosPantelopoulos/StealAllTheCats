using Microsoft.EntityFrameworkCore;

namespace StealAllTheCats.Infrastructure.Shared.Paging;

public class PagedList<T>
{
    public PagedList() { }

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        if (pageNumber <= 0)
            throw new Exception("Page Number cannot be below 0.");

        if (pageSize < 0)
            throw new Exception("Page Size cannot be below 0.");

        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Items = items;
    }

    public List<T> Items { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        List<T> items;

        if (pageNumber is -1)
        {
            items = await source.ToListAsync();
            pageSize = count;
            pageNumber = 1;
        }
        else
            items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        if (count == 0)
            pageNumber = 0;

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}