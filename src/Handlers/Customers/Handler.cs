using BugStore.Data;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Customers;

public class Handler
{
    private readonly AppDbContext _context;

    public Handler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerResponseAll> GetAllAsync()
    {
        var query = _context.Customers.AsQueryable();

        var customers = await query.ToListAsync();

        var customersResponse = customers.Select(c => new CustomerResponse
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            Phone = c.Phone,
            BirthDate = c.BirthDate
        });

        return new CustomerResponseAll
        {
            Customers = customersResponse
        };
    }

    public async Task<GetByIdCustomersResponse?> GetByIdAsync(GetByIdCustomersRequest request)
    {
        var customer = await _context.Customers.FindAsync(request.Id);

        if (customer == null) return null;

        return new GetByIdCustomersResponse
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            BirthDate = customer.BirthDate
        };

    }

    public async Task<CreateCustomersResponse> CreateCustomersAsync(CreateCustomersRequest customersRequest)
    {
        var entity = new Customer
        {
            Id = Guid.NewGuid(),
            Name = customersRequest.Name,
            Email = customersRequest.Email,
            Phone = customersRequest.Phone,
            BirthDate = customersRequest.BirthDate
        };

        _context.Customers.Add(entity);
        await _context.SaveChangesAsync();

        return new CreateCustomersResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            Phone = entity.Phone,
            BirthDate = entity.BirthDate
        };
    }
    
    public async Task<UpdateCustomersResponse?> UpdateAsync(Guid id, UpdateCustomersRequest request)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null) return null;

        if (request.Name != null)
            customer.Name = request.Name;
        if (request.Email != null)
            customer.Email = request.Email;
        if (request.Phone != null)
            customer.Phone = request.Phone;
        if (request.BirthDate.HasValue)
            customer.BirthDate = request.BirthDate.Value;
        await _context.SaveChangesAsync();

        return new UpdateCustomersResponse
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            BirthDate = customer.BirthDate
        };

    }

    public async Task<DeleteCustomersResponse?> DeleteAsync(DeleteCustomersRequest request)
    {
        var customer = await _context.Customers.FindAsync(request.Id);

        if (customer == null) return null;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return new DeleteCustomersResponse();
    }
}