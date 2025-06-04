using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IOrderService
{
    Task<List<OrderDTO>> GetOrders(string? lastName);   
}