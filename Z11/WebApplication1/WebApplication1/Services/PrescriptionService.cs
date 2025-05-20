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
        
        if (!_context.Doctors.Any(x => x.IdDoctor == prescription.IdDoctor))
            throw new DoctorNotFoundException($"Nie znaleziono lekara o id: {prescription.IdDoctor}");
        
        foreach (var medicament in prescription.Medicaments)
        {
            if (!_context.Medicaments.Any(x => x.IdMedicament == medicament.IdMedicament))
                throw new MedicamentNotFoundException($"Nie znalezione leków o id: {medicament.IdMedicament}");
        }


        Patient patient;
        
        if(!_context.Patients.Any(x => x.IdPatient == prescription.Patient.IdPatient))
        {
            patient = new Patient()
            {
                IdPatient = prescription.Patient.IdPatient,
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                BirthDate = prescription.Patient.BirthDate,
            };
            _context.Patients.Add(patient);
        }
        else
        {
            patient = _context.Patients.FirstOrDefault(x => x.IdPatient == prescription.Patient.IdPatient);
        }

        int newId = _context.Prescriptions.Max(x => x.IdPrescription) + 1;

        _context.Prescriptions.Add(new Prescription()
        {
            IdPrescription = newId,
            Patient = prescription.Patient,
            Doctor = _context.Doctors.First(x => x.IdDoctor == prescription.IdDoctor),
            Date = prescription.Date,
            DueDate = prescription.DueDate,
        });

        foreach (var medicament in prescription.Medicaments)
        {
            _context.Prescription_Medicaments.Add(new Prescription_Medicament()
            {
                IdMedicament = medicament.IdMedicament,
                IdPrescription = newId,
                Dose = medicament.Dose,
                Details = medicament.Description,
            });
        }
        
        await _context.SaveChangesAsync();

        return true;
    }
}