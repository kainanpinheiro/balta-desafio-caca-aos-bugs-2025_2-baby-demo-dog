namespace BugStore.Requests.Products;

public record CreateProductsRequest
{
    public string Title { get; init; }
    public string Description { get; init; }
    public string Slug { get; init; }
    public decimal Price { get; init; }
}