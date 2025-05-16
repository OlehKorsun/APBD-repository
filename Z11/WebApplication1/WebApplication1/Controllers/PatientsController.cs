using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.DAL;
using WebApplication.Models;

namespace WebApplication.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PatientsController : ControllerBase
{

    private readonly HospitalDbContext _dbContext;

    public PatientsController(HospitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("{idPatient}")]
    public async Task<IActionResult> GetPatientByIdAsync(int idPatient)
    {
        var result = await _dbContext.Patients
            .Include(p => p.Prescriptions)
            .ToListAsync();
        return Ok();
    }
    
}