using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs;

public class ClientDTO : ClientCreateDTO
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "IdClient musi być większe niż zero!")]
    public int IdClient { get; set; }
    public List<TripDTO> Trips { get; set; }
}


public class ClientCreateDTO
{
    [Required]
    [StringLength(120)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(120)]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [Phone]
    public string Telephone { get; set; }
    
    [Required]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "PESEL się składa z dokładnie 11 znaków!")]
    public string Pesel { get; set; }
}