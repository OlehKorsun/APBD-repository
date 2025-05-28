namespace Poprawka1.Models_DTOs;

public class PostRentalDTO
{
    public int IdRental { get; set; }
    public DateTime RentalDate { get; set; }
    public List<MovieDTO> Movies { get; set; }
}

public class PostMovieDTO
{
    public int IdMovie { get; set; }
    public decimal PriceAtRental { get; set; }
}