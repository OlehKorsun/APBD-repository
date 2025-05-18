using WebApplication.Models;

namespace WebApplication.Services;

public interface IPatientService
{
    Task<List<PatientDto>> GetPatients(int id);
}