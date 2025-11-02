namespace BugStore.Responses.Orders;

public record CreateOrdersResponse
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public List<OrdersLinesResponse> Lines { get; init; } = new();
}

public record OrdersLinesResponse
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal Total { get; init; }
}