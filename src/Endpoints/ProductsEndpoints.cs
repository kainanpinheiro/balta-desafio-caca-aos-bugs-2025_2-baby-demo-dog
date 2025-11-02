using BugStore.Handlers.Products;
using BugStore.Requests.Products;
using Microsoft.AspNetCore.Mvc;

namespace BugStore.Endpoints;

[ApiController]
[Route("v1/products")]
public class ProductsEndpoints : ControllerBase
{
    private readonly Handler _handler;

    public ProductsEndpoints(Handler handler)
    {
        _handler = handler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(await _handler.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var request = new GetByIdProductsRequest
        {
            Id = id
        };
        var result = await _handler.GetByIdAsync(request);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductsRequest request)
    {
        var result = await _handler.CreateAsync(request);

        return Created($"/v1/products/{result.Id}", result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductsRequest request)
    {
        var result = await _handler.UpdateAsync(id, request);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var request = new DeleteProductsRequest
        {
            Id = id
        };
        var result = await _handler.DeleteAsync(request);

        return result is null ? NotFound() : NoContent();
    }
}