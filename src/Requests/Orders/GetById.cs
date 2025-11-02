namespace BugStore.Requests.Orders;

public record GetByIdOrdersRequest
{
    public required Guid Id { get; init; }
}