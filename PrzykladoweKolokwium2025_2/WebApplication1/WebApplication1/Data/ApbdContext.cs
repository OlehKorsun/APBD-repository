using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public partial class ApbdContext : DbContext
{
    
    public DbSet<Client> Client { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Product_Order> Product_Order { get; set; }
    public DbSet<Status> Status { get; set; }
    
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
        // Sees
        modelBuilder.Entity<Status>().HasData(new List<Status>()
        {
            new Status(){Id = 1, Name = "Created"},
            new Status(){Id = 2, Name = "Ongoing"},
            new Status(){Id = 3, Name = "Completed"},
        });

        modelBuilder.Entity<Client>().HasData(new List<Client>()
        {
            new Client(){Id = 1, FirstName = "John", LastName = "Doe"},
            new Client(){Id = 2, FirstName = "Jane", LastName = "Doe"},
            new Client(){Id = 3, FirstName = "Julie", LastName = "Doe"},
        });

        modelBuilder.Entity<Product>().HasData(new List<Product>()
        {
            new Product(){Id = 1, Name = "Product1", Price = 15.4 },
            new Product(){Id = 2, Name = "Product2", Price = 1.4 },
            new Product(){Id = 3, Name = "Product3", Price = 145.4 },
        });

        modelBuilder.Entity<Order>().HasData(new List<Order>()
        {
            new Order() { Id = 1, CreatedAt = DateTime.Parse("2025-05-01"), FulfilledAt = DateTime.Parse("2025-05-02"), ClientId = 1, StatusId = 3},
            new Order() { Id = 2, CreatedAt = DateTime.Parse("2025-05-02"), FulfilledAt = null, ClientId = 1, StatusId = 2},
            new Order() { Id = 3, CreatedAt = DateTime.Parse("2025-05-03"), FulfilledAt = null, ClientId = 1, StatusId = 1},
            new Order() { Id = 4, CreatedAt = DateTime.Parse("2025-05-04"), FulfilledAt = null, ClientId = 2, StatusId = 1},
        });

        modelBuilder.Entity<Product_Order>().HasData(new List<Product_Order>()
        {
            new Product_Order() { ProductId = 1, OrderId = 1, Amount = 3},
            new Product_Order() { ProductId = 2, OrderId = 1, Amount = 5},
            new Product_Order() { ProductId = 3, OrderId = 1, Amount = 8},
            new Product_Order() { ProductId = 3, OrderId = 2, Amount = 1},
            new Product_Order() { ProductId = 2, OrderId = 2, Amount = 2},
            new Product_Order() { ProductId = 3, OrderId = 3, Amount = 8},
            new Product_Order() { ProductId = 1, OrderId = 3, Amount = 12},
        });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
