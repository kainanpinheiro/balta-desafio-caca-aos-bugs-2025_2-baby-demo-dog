namespace BugStore.Responses.Customers;

public record CustomerResponseAll
{
    public IEnumerable<CustomerResponse> Customers { get; init; } = Array.Empty<CustomerResponse>();
}

public record CustomerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
}