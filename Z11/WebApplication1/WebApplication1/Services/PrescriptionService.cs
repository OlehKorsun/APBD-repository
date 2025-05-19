using WebApplication.DAL;
using WebApplication.Exceptions;
using WebApplication.Models;

namespace WebApplication.Services;

public class PrescriptionService : IPrescriptionService
{
    
    private readonly HospitalDbContext _context;
    public PrescriptionService(HospitalDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddPrescription(CreatePrescriptionDto prescription)
    {
        if (prescription.Date > prescription.DueDate)
            throw new DateException("Date nie może być wcześniej niż DueDate");

        if (prescription.Medicaments.Count > 10)
            throw new MedicamentLimitException("Przekroczono limit leków na receptę! Nie można podać więcej niż 10 leków!");
        
        if (!_context.Doctors.Any(x => x.IdDoctor == prescription.IdDoctor))
            throw new DoctorNotFoundException($"Nie znaleziono lekara o id: {prescription.IdDoctor}");


        foreach (var medicament in prescription.Medicaments)
        {
            if (!_context.Medicaments.Any(x => x.IdMedicament == medicament.IdMedicament))
                throw new MedicamentNotFoundException($"Nie znalezione leków o id: {medicament.IdMedicament}");
        }
        
        return false;
    }
}