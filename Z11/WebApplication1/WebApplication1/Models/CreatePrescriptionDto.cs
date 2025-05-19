namespace WebApplication.Models;

public class CreatePrescriptionDto
{
    public Patient Patient { get; set; }
    public int IdDoctor { get; set; }
    public ICollection<CreateMedicamentsDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}

public class CreatePatientDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class CreateMedicamentsDto
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; }
}