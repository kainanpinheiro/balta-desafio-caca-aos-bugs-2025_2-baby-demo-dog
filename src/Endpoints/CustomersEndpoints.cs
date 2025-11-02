using BugStore.Handlers.Customers;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using Microsoft.AspNetCore.Mvc;

namespace BugStore.Endpoints;

[ApiController]
[Route("v1/customers")]
public class CustomersEndpoints : ControllerBase
{
    private readonly Handler _handler;

    public CustomersEndpoints(Handler handler)
    {
        _handler = handler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        return Ok(await _handler.GetAllAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomersById(Guid id)
    {
        var request = new GetByIdCustomersRequest
        {
            Id = id
        };
        var result = await _handler.GetByIdAsync(request);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomers(
        CreateCustomersRequest createCustomersRequest)
    {
        var result = await _handler.CreateCustomersAsync(createCustomersRequest);

        return Created($"/v1/customers/{result.Id}", result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomers(Guid id, UpdateCustomersRequest request)
    {
        var result = await _handler.UpdateAsync(id, request);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomers(Guid id)
    {
        var request = new DeleteCustomersRequest
        {
            Id = id
        };

        var result = await _handler.DeleteAsync(request);

        return result is null ? NotFound() : Ok();
    }
}