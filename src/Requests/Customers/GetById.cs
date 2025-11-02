namespace BugStore.Requests.Customers;

public record GetByIdCustomersRequest
{
    public required Guid Id { get; init; }
}