using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IOrderService
{
    Task<OrderDTO> GetOrder(int id);
    Task PutOrder(int id, OrderRequest request);
}