using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;

namespace WebApplication1.Services;

public class OrderService : IOrderService
{
    private readonly ApbdContext _context;

    public OrderService(ApbdContext context)
    {
        _context = context;
    }

    public async Task<OrderDTO> GetOrder(int id)
    {
        var order = await _context.Order
            .Include(s => s.Status)
            .Include(c => c.Client)
            .Include(p => p.ProductOrders)
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (order == null)
        {
            throw new OrderNotFoundException($"Nie znaleziono zamówienia o ID: {id}");
        }

        var result = new OrderDTO()
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            Status = order.Status.Name,
            Client = new ClientDTO()
            {
                FirstName = order.Client.FirstName,
                LastName = order.Client.LastName,
            },
            Products = order.ProductOrders.Select(p => new ProductDTO()
            {
                Name = p.Product.Name,
                Price = p.Product.Price,
                Amount = p.Amount
            }).ToList(),
        };
        return result;
    }
}