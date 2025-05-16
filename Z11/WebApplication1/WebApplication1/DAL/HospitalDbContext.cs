using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.DAL;

public class HospitalDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }

    protected HospitalDbContext()
    {
    }

    public HospitalDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Seed 
        
        // Seed Doctor
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor
            { 
                IdDoctor = 1,
                FirstName = "John",
                 LastName = "Doe",
                Email = "john@example.com",
            },
            new Doctor
            {
                IdDoctor = 2,
                FirstName = "Jane",
                LastName = "Judzy",
                Email = "jane@example.com",
            },
            new Doctor
            {
                IdDoctor = 3,
                FirstName = "Bill",
                LastName = "De",
                Email = "bill@example.com",
            }
        );
    }
}