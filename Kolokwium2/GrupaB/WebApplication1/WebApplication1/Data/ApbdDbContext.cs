using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApbdDbContext : DbContext
{
    public virtual DbSet<Concert> Concert { get; set; }
    public virtual DbSet<Customer> Customer { get; set; }
    public virtual DbSet<Ticket> Ticket { get; set; }
    public virtual DbSet<PurchasedTicket> PurchasedTicket { get; set; }
    public virtual DbSet<TicketConcert> TicketConcert { get; set; }

    protected ApbdDbContext()
    {
    }

    public ApbdDbContext(DbContextOptions options) : base(options)
    {
    }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Concert>().HasData(new List<Concert>()
        {
            new Concert(){ConcertId = 1, Name = "Concert 1", AvailableTickets = 5, Date = DateTime.Parse("2024-05-05")},
            new Concert(){ConcertId = 2, Name = "Concert 2", AvailableTickets = 7, Date = DateTime.Parse("2024-05-07")},
            new Concert(){ConcertId = 3, Name = "Concert 3", AvailableTickets = 10, Date = DateTime.Parse("2024-05-10")},
        });

        modelBuilder.Entity<Customer>().HasData(new List<Customer>()
        {
            new Customer(){CustomerId = 1, FirstName = "John", LastName = "Doe"},
            new Customer(){CustomerId = 2, FirstName = "Jane", LastName = "Doe"},
            new Customer(){CustomerId = 3, FirstName = "John", LastName = "Doe"},
        });

        modelBuilder.Entity<Ticket>().HasData(new List<Ticket>()
        {
            new Ticket(){TicketId = 1, SeatNumber = 54, SerialNumber = "1"},
            new Ticket(){TicketId = 2, SeatNumber = 68, SerialNumber = "2"},
            new Ticket(){TicketId = 3, SeatNumber = 12, SerialNumber = "3"},
        });

        modelBuilder.Entity<TicketConcert>().HasData(new List<TicketConcert>()
        {
            new TicketConcert(){TicketConcertId = 1, TicketId = 1, ConcertId = 1, Price = 56.99},
            new TicketConcert(){TicketConcertId = 2, TicketId = 2, ConcertId = 2, Price = 78.99},
            new TicketConcert(){TicketConcertId = 3, TicketId = 3, ConcertId = 3, Price = 15.99}
        });


        modelBuilder.Entity<PurchasedTicket>().HasData(new List<PurchasedTicket>()
        {
            new PurchasedTicket(){TicketConcertId = 1, CustomerId = 1, PurchaseDate = DateTime.Parse("2024-05-04")},
            new PurchasedTicket(){TicketConcertId = 2, CustomerId = 2, PurchaseDate = DateTime.Parse("2024-05-03")},
            new PurchasedTicket(){TicketConcertId = 3, CustomerId = 3, PurchaseDate = DateTime.Parse("2024-05-05")},
        });
    }
    
}