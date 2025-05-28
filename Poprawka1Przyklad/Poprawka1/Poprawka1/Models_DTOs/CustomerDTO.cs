namespace Poprawka1.Models_DTOs;

public class CustomerDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<RentalsDTO> Rentals { get; set; }
}

public class RentalsDTO
{
    public int Id { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string Status { get; set; }
    public List<MovieDTO> Movies { get; set; }
}

public class MovieDTO
{
    public string Title { get; set; }
    public decimal PriceAtRental { get; set; }
}