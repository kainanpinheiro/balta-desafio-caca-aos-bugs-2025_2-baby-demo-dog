namespace BugStore.Responses.Orders;

public class GetByIdOrdersResponse
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public List<OrdersLinesResponse> Lines { get; init; } = new();
}