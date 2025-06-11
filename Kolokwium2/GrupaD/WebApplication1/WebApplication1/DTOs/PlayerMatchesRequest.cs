using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class PlayerMatchesRequest
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [Required]
    public DateTime BirthDate { get; set; }
    
    [Required]
    public List<MatchesRequest> Matches { get; set; }
}

public class MatchesRequest{
    [Required]
    public int MatchId { get; set; }
    
    [Required]
    public double Rating { get; set; }
    
    [Required]
    public int MVPs { get; set; }
    
}