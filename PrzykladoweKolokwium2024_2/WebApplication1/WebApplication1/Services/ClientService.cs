using System.Transactions;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class ClientService : IClientService
{
    
    private readonly ApbdContext _context;

    public ClientService(ApbdContext context)
    {
        _context = context;
    }
    public async Task AddOrder(int clientId, OrderRequest order)
    {
        var client = await _context.Clients.FindAsync(clientId);
        if (client == null)
        {
            throw new ClientNotFoundException($"Nie znaleziono klienta o ID: {clientId}");
        }
        var employee = await _context.Employees.FindAsync(order.EmployeeId);
        if (employee == null)
        {
            throw new EmployeeNotFoundException($"Nie znaleziono pracownika o ID: {order.EmployeeId}");
        }

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var newOrder = new Order()
            {
                AcceptedAt = order.AcceptedAt,
                Comments = order.Comments,
                Employee = employee,
                Client = client,
                PastryOrders = new List<Order_Pastry>()
            };
        
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
        
            List<Order_Pastry> pastries = new List<Order_Pastry>();
            foreach (var pastry in order.Pastries)
            {
                var tmp = await _context.Pastries.FirstOrDefaultAsync(p => p.Name == pastry.Name);
                if (tmp == null)
                {
                    throw new ProductNotFoundException($"Nie znaleziono produktu o nazwie: {pastry.Name}");
                }
                pastries.Add(new Order_Pastry()
                {
                    Amount = pastry.Amount,
                    Comments = pastry.Comments,
                    Order = newOrder,
                    Pastry = tmp,
                });
            }
            newOrder.PastryOrders = pastries;
            await _context.SaveChangesAsync();
            
            scope.Complete();
        }
    }
}