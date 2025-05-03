using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs;

public class TripDTO
{
    public int IdTrip {get; set;}
    
    [Required]
    [StringLength(120)]
    public string Name {get; set;}
    
    [Required]
    [StringLength(220)]
    public string Description {get; set;}
    
    [Required]
    public DateTime DateFrom {get; set;}
    
    [Required]
    public DateTime DateTo {get; set;}
    
    [Required]
    [Range(1, int.MaxValue)]
    public int MaxPeople {get; set;}
    public List<CountryDTO> Countries { get; set; }

    public TripDTO()
    {
        Countries = new List<CountryDTO>(); 
    }
}


public class ClientTripDTO : TripDTO
{
    [Required]
    public int RegisteredAt {get; set;}
    public int? PaymentDate {get; set;}
}



public class CountryDTO
{
    [Required]
    [StringLength(120)]
    public string Name {get; set;}

    public CountryDTO(string name)
    {
        Name = name;
    }
}


