namespace BugStore.Requests.Products;

public record DeleteProductsRequest
{
    public required Guid Id { get; init; }
}