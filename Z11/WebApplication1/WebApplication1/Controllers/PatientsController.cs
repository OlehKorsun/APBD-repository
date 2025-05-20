using Microsoft.AspNetCore.Mvc;
using WebApplication.Exceptions;
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
        try
        {
            var result = _patientService.GetPatients(idPatient);
            return Ok(result.Result);
        }
        catch (PatientNotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}