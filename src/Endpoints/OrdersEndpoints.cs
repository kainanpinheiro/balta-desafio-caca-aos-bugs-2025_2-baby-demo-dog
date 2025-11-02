using BugStore.Handlers.Orders;
using BugStore.Requests.Orders;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BugStore.Endpoints;

[ApiController]
[Route("v1/orders")]
public class OrdersEndpoints : ControllerBase
{
    private readonly Handler _handler;

    public OrdersEndpoints(Handler handler)
    {
        _handler = handler;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var request = new GetByIdOrdersRequest
        {
            Id = id
        };

        var result = await _handler.GetByIdAsync(request);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrdersRequest request)
    {
        var result = await _handler.CreateAsync(request);

        return result is null ? NotFound() : Created($"/v1/orders/{result.Id}", result);
    }
}