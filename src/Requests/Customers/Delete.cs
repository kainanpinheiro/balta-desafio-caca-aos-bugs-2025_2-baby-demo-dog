namespace BugStore.Requests.Customers;

public record DeleteCustomersRequest
{
    public required Guid Id { get; init; }
}