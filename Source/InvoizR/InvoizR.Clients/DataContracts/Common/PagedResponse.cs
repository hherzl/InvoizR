namespace InvoizR.Clients.DataContracts.Common;

public record PagedResponse<TModel> where TModel : class
{
    public PagedResponse()
    {
    }

    public PagedResponse(IEnumerable<TModel> data)
    {
        Model = [.. data];
    }

    public PagedResponse(IEnumerable<TModel> data, int pageSize, int pageNumber, int itemsCount)
    {
        Model = [.. data];

        PageSize = pageSize;
        PageNumber = pageNumber;
        ItemsCount = itemsCount;
    }

    public List<TModel> Model { get; set; }

    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int ItemsCount { get; set; }

    public double PageCount
        => ItemsCount < PageSize ? 1 : (int)(double)ItemsCount / PageSize;
}
