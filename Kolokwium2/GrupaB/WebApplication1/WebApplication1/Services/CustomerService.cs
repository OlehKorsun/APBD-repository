using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class CustomerService : ICustomerService
{
    
    private static int serialNumberForTicket = 1;
    
    private readonly ApbdDbContext _context;

    public CustomerService(ApbdDbContext context)
    {
        _context = context;
    }
    
    public async Task<CustomerDTO> GetCustomerByIdAsync(int id)
    {
        
        var customer = await _context.Customer.FindAsync(id);
        if (customer == null)
        {
            throw new KeyNotFoundException($"Nie znaleziono klienta o id: {id}");
        }
        
        var customers = await _context.Customer
            .Include(p => p.PurchasedTickets)
            .Include(p => p.PurchasedTickets)
                .ThenInclude(t => t.TicketConcert)
            .Include(p => p.PurchasedTickets)
                .ThenInclude(tc => tc.TicketConcert)
                .ThenInclude(t => t.Ticket)
            .Include(p => p.PurchasedTickets)
                .ThenInclude(tc => tc.TicketConcert)
                .ThenInclude(c => c.Concert).Where(a => a.CustomerId == id)
            .ToListAsync();
        
        var result = customers.Select(c => new CustomerDTO()
        {
            firstName = c.FirstName,
            lastName = c.LastName,
            PhoneNumber = c.PhoneNumber,
            Purchases = c.PurchasedTickets.Select(p => new PurchasesDTO()
            {
                Date = p.TicketConcert.Concert.Date,
                Price = p.TicketConcert.Price,
                Ticket = new TicketDTO()
                {
                    Serial = p.TicketConcert.Ticket.SerialNumber,
                    SeatNumber = p.TicketConcert.Ticket.SeatNumber,
                },
                Concert = new ConcertDTO()
                {
                    Date = p.TicketConcert.Concert.Date,
                    Name = p.TicketConcert.Concert.Name
                }
            }).ToList(),
        }).ToList();

        if (result.Count.Equals(0))
        {
            var cust = new CustomerDTO()
            {
                firstName = customer.FirstName,
                lastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Purchases = new List<PurchasesDTO>(),
            };
            return cust;
        }
        
        return result.FirstOrDefault();
    }


    public async Task PostCustomerAsync(CustomerTicketsRequest customerRequest)
    {
        
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {

            // var concerts = new List<Concert>();
            // var tickets = new List<Ticket>();
            var ticketConcerts = new List<TicketConcert>();
        
            foreach (var ticket in customerRequest.Purchases)
            {
                var concert = await _context.Concert.FirstOrDefaultAsync(c => c.Name == ticket.ConcertName);
                if (concert == null)
                {
                    throw new ConcertNotFoundException($"Nie znaleziono koncertu o nazwie: {ticket.ConcertName}");
                }
                // concerts.Add(concert);

                var newTicket = new Ticket()
                {
                    SerialNumber = "T" + serialNumberForTicket++ + concert.Name.Substring(0, 1),
                    TicketConcerts = new List<TicketConcert>(),
                    SeatNumber = ticket.SeatNumber,
                };
                // tickets.Add(newTicket);
                await _context.Ticket.AddAsync(newTicket);

                var ticketConcert = new TicketConcert()
                {
                    Ticket = newTicket,
                    Concert = concert,
                    Price = ticket.Price
                };
                ticketConcerts.Add(ticketConcert);
                await _context.TicketConcert.AddAsync(ticketConcert);

            }
            await _context.SaveChangesAsync();
            
            
            
            
            
            var newCustomer = await _context.Customer.FindAsync(customerRequest.Customer.Id);
        
            if (newCustomer == null)
            {
                newCustomer = new Customer()
                {
                    FirstName = customerRequest.Customer.FirstName,
                    LastName = customerRequest.Customer.LastName,
                    PhoneNumber = customerRequest.Customer.PhoneNumber,
                };
                await _context.Customer.AddAsync(newCustomer);
                await _context.SaveChangesAsync();
            }

            



            foreach (var ticketConcert in ticketConcerts)
            {
                var newPurchasedTicket = new PurchasedTicket()
                {
                    CustomerId = newCustomer.CustomerId,
                    TicketConcertId = ticketConcert.TicketConcertId,
                    PurchaseDate = DateTime.Now,
                };
                
                await _context.PurchasedTicket.AddAsync(newPurchasedTicket);
            }
            
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
        
    }
    
}