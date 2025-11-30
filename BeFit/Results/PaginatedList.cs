namespace BeFit.Results;

public record PaginatedList<T>(
    IReadOnlyList<T> Items,
    int TotalItems,
    int PageNumber,
    int PageSize
)
{
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
}
