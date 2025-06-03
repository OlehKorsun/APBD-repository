using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        try
        {
            var order = await _orderService.GetOrder(id);
            return Ok(order);
        }
        catch (OrderNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }


    [HttpPut("{id}/fulfill")]
    public async Task<IActionResult> PutOrder(int id, [FromBody] OrderRequest request)
    {
        try
        {
            await _orderService.PutOrder(id, request);
            return Ok();
        }
        catch (OrderNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (StatusNotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (OrderComplitedException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}