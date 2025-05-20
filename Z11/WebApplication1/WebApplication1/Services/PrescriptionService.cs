using Microsoft.EntityFrameworkCore;
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
            throw new DateException("Date nie może być później niż DueDate");

        if (prescription.Medicaments.Count > 10)
            throw new MedicamentLimitException($"Przekroczono limit leków na receptę! Nie można podać więcej niż 10 leków! Podano: {prescription.Medicaments.Count}");
        
        if (! await _context.Doctors.AnyAsync(x => x.IdDoctor == prescription.IdDoctor))
            throw new DoctorNotFoundException($"Nie znaleziono lekara o id: {prescription.IdDoctor}");

        // sprawdzam czy istnieje Pacjent, jeżeli nie, tworzę go i dodaję do bazy
        var patient = await _context.Patients.FindAsync(prescription.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient()
            {
                IdPatient = prescription.Patient.IdPatient,
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                BirthDate = prescription.Patient.Birthdate,
                Prescriptions = new List<Prescription>()
            };
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }
        
        
        // sprawdzam czy istnieją wszystkie podane leki
        var medicamentIds = prescription.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMedicaments = await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();
        
        var missing = medicamentIds.Except(existingMedicaments).ToList();
        if (missing.Any())
        {
            throw new MedicamentNotFoundException($"Nie znaleziono leków o id: {string.Join(", ", missing)}");
        }
        

        Prescription newPrescription = new Prescription()
        {
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = prescription.IdDoctor,
            Prescription_Medicaments = prescription.Medicaments.Select(m => new Prescription_Medicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Description
            }).ToList()
        };
        
        await _context.Prescriptions.AddAsync(newPrescription);
        await _context.SaveChangesAsync();

        return true;
    }
}