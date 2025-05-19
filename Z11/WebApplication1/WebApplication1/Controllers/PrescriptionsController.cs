using Microsoft.AspNetCore.Mvc;
using WebApplication.Exceptions;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService, CancellationToken cancellationToken)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> PostPrescription([FromBody]CreatePrescriptionDto prescription, CancellationToken cancellationToken)
    {
        try
        {
            var res = _prescriptionService.AddPrescription(prescription);
            if (!res.Result)
            {
                return BadRequest(res);
            }
            return Ok();
        }
        catch (MedicamentNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (MedicamentLimitException e)
        {
            return BadRequest(e.Message);
        }
        catch (DateException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}