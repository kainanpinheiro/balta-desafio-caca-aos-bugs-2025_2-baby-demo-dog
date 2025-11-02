using BugStore.Data;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Requests.Orders;
using BugStore.Responses.Orders;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Orders;

public class Handler
{
    private readonly AppDbContext _context;

    public Handler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GetByIdOrdersResponse?> GetByIdAsync(GetByIdOrdersRequest request)
    {
        var order = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Lines)
                .ThenInclude(l => l.Product)
            .FirstOrDefaultAsync(o => o.Id == request.Id);

        if (order is null) return null;

        return new GetByIdOrdersResponse
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            Lines = order.Lines.Select(l => new OrdersLinesResponse
            {
                Id = l.Id,
                ProductId = l.ProductId,
                Quantity = l.Quantity,
                Total = l.Total
            }).ToList(),
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt
        };
    }

    public async Task<CreateOrdersResponse?> CreateAsync(CreateOrdersRequest request)
    {
        var customers = await _context.Customers.FindAsync(request.CustomerId);
        if (customers is null) return null;
        
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Lines = new()
        };

        foreach (var line in request.Lines)
        {
            var product = await _context.Products.FindAsync(line.ProductId);

            if (product is null) return null;

            var orderLine = new OrderLine
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = line.ProductId,
                Quantity = line.Quantity,
                Total = product.Price * line.Quantity
            };
            
            order.Lines.Add(orderLine);
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return new CreateOrdersResponse
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            Lines = order.Lines.Select(l => new OrdersLinesResponse
            {
                Id = l.Id,
                ProductId = l.ProductId,
                Quantity = l.Quantity,
                Total = l.Total
            }).ToList(),
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt
        };
    }
}