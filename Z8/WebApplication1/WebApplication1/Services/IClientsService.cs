﻿using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services;

public interface IClientsService
{
    Task<List<ClientDTO>> GetClientsAsync();
    
    Task<List<ClientTripDTO>> GetClientTripsByClientId(int id);
    
    Task<int> CreateClientAsync(ClientCreateDTO client);
    
    
    Task<bool> ZarejestrujKlientaNaWycieczke(int id, int tripId);
    
    Task<bool> DeleteClientTrip(int id, int tripId);
}