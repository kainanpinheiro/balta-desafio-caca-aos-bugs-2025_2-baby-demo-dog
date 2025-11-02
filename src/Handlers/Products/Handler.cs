using BugStore.Data;
using BugStore.Models;
using BugStore.Requests.Products;
using BugStore.Responses.Products;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Products;

public class Handler
{
    private readonly AppDbContext _context;

    public Handler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GetAllProductsResponse> GetAllAsync()
    {
        var query = _context.Products.AsQueryable();

        var products = await query.ToListAsync();

        var productsResponse = products.Select(p => new ProductsResponse
        {
            Id = p.Id,
            Description = p.Description,
            Price = p.Price,
            Slug = p.Slug,
            Title = p.Title
        });

        return new GetAllProductsResponse
        {
            products = productsResponse
        };
    }

    public async Task<GetByIdProductsResponse?> GetByIdAsync(GetByIdProductsRequest request)
    {
        var products = await _context.Products.FindAsync(request.Id);

        if (products is null) return null;

        return new GetByIdProductsResponse
        {
            Id = products.Id,
            Description = products.Description,
            Price = products.Price,
            Slug = products.Slug,
            Title = products.Title
        };
    }

    public async Task<CreateProductsResponse> CreateAsync(CreateProductsRequest request)
    {
        var products = new Product()
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            Slug = request.Slug
        };

        _context.Products.Add(products);
        await _context.SaveChangesAsync();

        return new CreateProductsResponse
        {
            Id = products.Id,
            Description = products.Description,
            Price = products.Price,
            Slug = products.Slug,
            Title = products.Title
        };
    }

    public async Task<UpdateProductsResponse?> UpdateAsync(Guid id, UpdateProductsRequest request)
    {
        var products = await _context.Products.FindAsync(id);

        if (products is null) return null;

        if (request.Description != null)
            products.Description = request.Description;

        if (request.Title != null)
            products.Title = request.Title;

        if (request.Slug != null)
            products.Slug = request.Slug;

        if (request.Price.HasValue)
            products.Price = request.Price.Value;

        await _context.SaveChangesAsync();

        return new UpdateProductsResponse
        {
            Id = products.Id,
            Description = products.Description,
            Price = products.Price,
            Slug = products.Slug,
            Title = products.Title
        };
    }

    public async Task<DeleteProductsResponse?> DeleteAsync(DeleteProductsRequest request)
    {
        var products = await _context.Products.FindAsync(request.Id);

        if (products is null) return null;

        _context.Products.Remove(products);
        await _context.SaveChangesAsync();

        return new DeleteProductsResponse();
    }
}