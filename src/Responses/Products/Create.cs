namespace BugStore.Responses.Products;

public record CreateProductsResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Slug { get; init; }
    public decimal Price { get; init; }
}