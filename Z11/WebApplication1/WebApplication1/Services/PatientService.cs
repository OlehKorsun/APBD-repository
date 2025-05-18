using Microsoft.EntityFrameworkCore;
using WebApplication.DAL;
using WebApplication.Models;

namespace WebApplication.Services;

public class PatientService: IPatientService
{
    
    private readonly HospitalDbContext _context;
    public PatientService(HospitalDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<PatientDto>> GetPatients(int id)
    {
        var result = await _context.Patients
            .Include(p => p.Prescriptions)
                .ThenInclude(p => p.Doctor)
            .Include(p => p.Prescriptions)
                .ThenInclude(p => p.Prescription_Medicaments)
                    .ThenInclude(pm => pm.Medicament)
            .Where(p => p.IdPatient == id)
            .Select( e => new PatientDto
                {
                    IdPatient = e.IdPatient,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DateOfBirth = e.BirthDate,
                    Prescriptions = e.Prescriptions.Select(p => new PrescriptionDto
                    {
                        IdPrescription = p.IdPrescription,
                        Date = p.Date,
                        DueDate = p.DueDate,
                        Medicaments = p.Prescription_Medicaments.Select(m => new MedicamentDto
                        {
                            IdMedicament = m.Medicament.IdMedicament,
                            Name = m.Medicament.Name,
                            Dose = m.Dose,
                            Description = m.Medicament.Description,
                            
                        }).ToList(),
                        Doctor = new DoctorDto
                        {
                            IdDoctor = p.Doctor.IdDoctor,
                            FirstName = p.Doctor.FirstName,
                        },
                    }).OrderBy(g => g.DueDate).ToList(),
                }).ToListAsync();
        
        return result;
    }
}