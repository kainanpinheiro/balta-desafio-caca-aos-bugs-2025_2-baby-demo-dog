namespace BugStore.Requests.Products;

public record GetByIdProductsRequest
{
    public required Guid Id { get; init; }
}