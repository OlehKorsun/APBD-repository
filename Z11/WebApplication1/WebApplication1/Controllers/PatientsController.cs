using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.DAL;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PatientsController : ControllerBase
{

    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }
    
    [HttpGet("{idPatient}")]
    public async Task<IActionResult> GetPatientByIdAsync(int idPatient)
    {
        var result = _patientService.GetPatients(idPatient);

        if (result.Result == null)
        {
            return NotFound();
        }
        
        return Ok(result.Result);
    }
    
}