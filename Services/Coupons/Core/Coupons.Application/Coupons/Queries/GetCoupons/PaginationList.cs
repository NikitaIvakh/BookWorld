namespace Coupons.Application.Coupons.Queries.GetCoupons;

public sealed class PaginationList<T>
{
    private PaginationList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public List<T> Items { get; }

    public int Page { get; }

    public int PageSize { get; }

    public int TotalCount { get; }

    public bool HasNextPage => Page * PageSize < TotalCount;

    public bool HasPreviousPage => Page > 1;

    public static async Task<PaginationList<T>> CreateAsync(List<T> items, int page, int pageSize)
    {
        var totalCount = items.Count();
        var query = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return new PaginationList<T>(query, page, pageSize, totalCount);
    }
}