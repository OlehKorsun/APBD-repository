using WebApplication.Models;

namespace WebApplication.Services;

public interface IPrescriptionService
{
    Task<bool> AddPrescription(CreatePrescriptionDto prescription);
}