namespace PrzykladoweKolokwium2025_1.Models;

public class CustomerDTO
{
    public int customer_id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public List<RentalDTO> rentals { get; set; }
}