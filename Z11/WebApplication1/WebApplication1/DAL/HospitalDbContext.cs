using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.DAL;

public class HospitalDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }

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
        
        
        
        
        
        // Sees Patient
        modelBuilder.Entity<Patient>().HasData(
            new Patient()
            {
                IdPatient = 1,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(2000, 1, 1),
            },
            
            new Patient()
            {
                IdPatient = 2,
                FirstName = "Jane",
                LastName = "Judzy",
                BirthDate = new DateTime(2000, 2, 1),
            },
            
            new Patient()
            {
                IdPatient = 3,
                FirstName = "Bill",
                LastName = "De",
                BirthDate = new DateTime(2000, 3, 1),
            }
        );
        
        
        
        
        
        // Sees Prescription
        modelBuilder.Entity<Prescription>().HasData(
            new Prescription()
            {
                IdPrescription = 1,
                Date = new DateTime(2025, 5, 5),
                DueDate = new DateTime(2025, 5, 6),
                IdPatient = 1,
                IdDoctor = 1,
            },
            
            new Prescription()
            {
                IdPrescription = 2,
                Date = new DateTime(2025, 5, 5),
                DueDate = new DateTime(2025, 5, 6),
                IdPatient = 2,
                IdDoctor = 2,
            },
            
            new Prescription()
            {
                IdPrescription = 3,
                Date = new DateTime(2025, 5, 5),
                DueDate = new DateTime(2025, 5, 6),
                IdPatient = 3,
                IdDoctor = 3,
            }
        );


        
        // Sees Medicaments
        modelBuilder.Entity<Medicament>().HasData(
            new Medicament()
            {
                IdMedicament = 1,
                Description = "Description 1",
                Name = "Medicament 1",
                Type = "Type 1"
            },
            
            new Medicament()
            {
                IdMedicament = 2,
                Description = "Description 2",
                Name = "Medicament 2",
                Type = "Type 2"
            },
            
            new Medicament()
            {
                IdMedicament = 3,
                Description = "Description 3",
                Name = "Medicament 3",
                Type = "Type 3"
            }
        );


        modelBuilder.Entity<Prescription_Medicament>().HasData(
            new Prescription_Medicament()
            {
                IdMedicament = 1,
                IdPrescription = 1,
                Dose = 1,
                Details = "Details 1",
            },
            
            new Prescription_Medicament()
            {
                IdMedicament = 2,
                IdPrescription = 2,
                Dose = 1,
                Details = "Details 2",
            }
        );
        
        


    }
}