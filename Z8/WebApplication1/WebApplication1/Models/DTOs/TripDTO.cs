﻿namespace WebApplication1.Models.DTOs;

public class TripDTO
{
    public int IdTrip {get; set;}
    public string Name {get; set;}
    public List<CountryDTO> Countries { get; set; }
}



public class CountryDTO
{
    public string Name {get; set;}
}


