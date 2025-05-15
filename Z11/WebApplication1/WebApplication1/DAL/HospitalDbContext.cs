using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.DAL;

public class HospitalDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }

    protected HospitalDbContext()
    {
    }

    public HospitalDbContext(DbContextOptions options) : base(options)
    {
    }
}