using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public partial class ApbdContext : DbContext
{
    
    public DbSet<Order> Orders { get; set; }
    public DbSet<Order_Pastry> PastryOrders { get; set; }
    public DbSet<Pastry> Pastries { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
    public ApbdContext()
    {
    }

    public ApbdContext(DbContextOptions<ApbdContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<Client>().HasData(new List<Client>()
        {
            new Client(){ID = 1, FirstName = "CJohn", LastName = "Doe"},
            new Client(){ID = 2, FirstName = "CJane", LastName = "Doe"},
            new Client(){ID = 3, FirstName = "CJohn", LastName = "Doe"},
        });

        modelBuilder.Entity<Employee>().HasData(new List<Employee>()
        {
            new Employee(){ID = 1, FirstName = "EJohn", LastName = "Doe"},
            new Employee(){ID = 2, FirstName = "EJane", LastName = "Doe"},
            new Employee(){ID = 3, FirstName = "EJohn", LastName = "Doe"},
        });

        modelBuilder.Entity<Pastry>().HasData(new List<Pastry>()
        {
            new Pastry(){ID = 1, Price = 0.99, Type = "TypeA", Name = "Pastry A"},
            new Pastry(){ID = 2, Price = 1.59, Type = "TypeB", Name = "Pastry B"},
            new Pastry(){ID = 3, Price = 6.91, Type = "TypeC", Name = "Pastry C"},
        });

        modelBuilder.Entity<Order>().HasData(new List<Order>()
        {
            new Order(){ID = 1, AcceptedAt = DateTime.Parse("2024-05-28"), FulfilledAt = DateTime.Parse("2024-05-29"), Comments = "CommentA", ClientID = 1, EmployeeID = 2},
            new Order(){ID = 2, AcceptedAt = DateTime.Parse("2024-06-20"), FulfilledAt = DateTime.Parse("2024-06-22"), Comments = "CommentB", ClientID = 2, EmployeeID = 1},
            new Order(){ID = 3, AcceptedAt = DateTime.Parse("2024-06-21"), FulfilledAt = DateTime.Parse("2024-06-22"), Comments = "CommentC", ClientID = 3, EmployeeID = 2},
            new Order(){ID = 4, AcceptedAt = DateTime.Parse("2024-06-21"), FulfilledAt = DateTime.Parse("2024-06-23"), Comments = "CommentD", ClientID = 1, EmployeeID = 3},
        });

        modelBuilder.Entity<Order_Pastry>().HasData(new List<Order_Pastry>()
        {
            new Order_Pastry(){OrderID = 1, PastryID = 1, Amount = 4, Comments = "Comment A"},
            new Order_Pastry(){OrderID = 1, PastryID = 3, Amount = 2, Comments = "Comment B"},
            new Order_Pastry(){OrderID = 2, PastryID = 2, Amount = 5},
            new Order_Pastry(){OrderID = 3, PastryID = 1, Amount = 1},
            new Order_Pastry(){OrderID = 4, PastryID = 1, Amount = 5},
            new Order_Pastry(){OrderID = 4, PastryID = 2, Amount = 2}
        });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
