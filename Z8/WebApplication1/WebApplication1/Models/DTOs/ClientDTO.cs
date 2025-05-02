using Microsoft.Build.Framework;

namespace WebApplication1.Models.DTOs;

public class ClientDTO : ClientCreateDTO
{
    public int IdClient { get; set; }
    public List<TripDTO> Trips { get; set; }
}


public class ClientCreateDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Pesel { get; set; }
}