namespace PrzykladoweKolokwium2025_1.Models;

public class RentalDTO : RentalClientDTO
{
    public DateTime returnDate { get; set; }
    public string status { get; set; }
}

public class RentalClientDTO
{
    public int id { get; set; }
    public DateTime rentalDate { get; set; }
    public List<MowieDTO> mowies { get; set; }
}