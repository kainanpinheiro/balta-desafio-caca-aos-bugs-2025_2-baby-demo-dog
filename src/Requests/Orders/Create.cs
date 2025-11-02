namespace BugStore.Requests.Orders;

public record CreateOrdersRequest
{
    public Guid CustomerId { get; init; }

    public List<OrderLineRequest> Lines { get; init; }
}

public record OrderLineRequest
{
    public int Quantity { get; init; }
    public Guid ProductId { get; init; }
}