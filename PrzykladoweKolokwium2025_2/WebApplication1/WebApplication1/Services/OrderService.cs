using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Models;

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


    public async Task PutOrder(int id, OrderRequest request)
    {
        var status = await _context.Status.FirstOrDefaultAsync(s => s.Name == request.StatusName);
        if (status == null)
        {
            throw new StatusNotFoundException($"Nie znaleziono statusu o nazwie: {request.StatusName}");
        }
        
        var order = await _context.Order.Include(s => s.Status).Include(p => p.ProductOrders).FirstOrDefaultAsync(o => o.Id == id);
        if (order == null)
        {
            throw new OrderNotFoundException($"Nie znaleziono zamówienia o ID: {id}");
        }
        if (order.Status.Name == "Completed")
        {
            throw new OrderComplitedException($"Zamówienie o ID: {id} już zostało zrealizowane");
        }
        
        order.StatusId = status.Id;
        order.FulfilledAt = DateTime.Now;
        order.ProductOrders.Clear();
        
        await _context.SaveChangesAsync();
    }
}