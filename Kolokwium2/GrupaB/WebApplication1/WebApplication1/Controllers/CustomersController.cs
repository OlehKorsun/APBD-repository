using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }


    [HttpGet("api/customers/{customerId}/purchases")]
    public async Task<IActionResult> GetCustomerByIdAsync(int customerId)
    {
        try
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            return Ok(customer);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }


    [HttpPost("customers")]
    public async Task<IActionResult> PostCustomerAsync([FromBody]CustomerTicketsRequest customerRequest)
    {
        try
        {
            await _customerService.PostCustomerAsync(customerRequest);
            return Created();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}