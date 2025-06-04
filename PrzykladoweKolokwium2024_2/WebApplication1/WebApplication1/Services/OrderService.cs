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
    public async Task<List<OrderDTO>> GetOrders(string? lastName)
    {
        var orders = await _context.Orders
            .Include(o => o.PastryOrders)
            .ThenInclude(o => o.Pastry)
            .Include(c => c.Client)
            .ToListAsync();

        if (lastName != null)
        {
            if (orders.All(o => o.Client.LastName != lastName))
            {
                throw new LastNameNotFoundException($"Nie znaleziono zamówień użytkownika o nazwisku: {lastName}");
            }
            orders = orders.Where(o => o.Client.LastName == lastName).ToList();
        }
        
        var result = orders.Select(o => new OrderDTO()
        {
            Id = o.ID,
            Comments = o.Comments,
            AcceptedAt = o.AcceptedAt,
            FulfilledAt = o.FulfilledAt,
            Pastries = o.PastryOrders.Select(p => 
                new PastryDTO()
                {
                    Name = p.Pastry.Name, Amount = p.Amount, Price = p.Pastry.Price
                }).ToList(),
        }).ToList();
        
        return result;
    }
}