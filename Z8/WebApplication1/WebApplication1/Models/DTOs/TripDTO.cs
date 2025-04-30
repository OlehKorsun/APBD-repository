namespace WebApplication1.Models.DTOs;

public class TripDTO
{
    public int IdTrip {get; set;}
    public string Name {get; set;}
    public string Description {get; set;}
    public DateTime DateFrom {get; set;}
    public DateTime DateTo {get; set;}
    public int MaxPeople {get; set;}
    public List<CountryDTO> Countries { get; set; }

    public TripDTO()
    {
        Countries = new List<CountryDTO>(); 
    }
}


public class ClientTripDTO : TripDTO
{
    public int RegisteredAt {get; set;}
    public int? PaymentDate {get; set;}
}



public class CountryDTO
{
    public string Name {get; set;}

    public CountryDTO(string name)
    {
        Name = name;
    }
}


