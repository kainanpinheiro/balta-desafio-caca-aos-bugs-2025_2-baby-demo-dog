namespace BugStore.Requests.Customers;

public record UpdateCustomersRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateTime? BirthDate { get; set; }
}