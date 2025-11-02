namespace BugStore.Responses.Products;

public record GetAllProductsResponse
{
    public IEnumerable<ProductsResponse> products { get; init; } = Array.Empty<ProductsResponse>();
}

public record ProductsResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Slug { get; init; }
    public decimal Price { get; init; }
}