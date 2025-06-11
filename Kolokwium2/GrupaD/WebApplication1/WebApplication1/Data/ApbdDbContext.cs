using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApbdDbContext : DbContext
{
    
    public DbSet<PlayerMatch> PlayerMatches { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<Map> Maps { get; set; }
    
    protected ApbdDbContext()
    {
    }

    public ApbdDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Map>().HasData(new List<Map>()
        {
            new Map() { MapId = 1, Name = "Map 1", Type = "Type 1" },
            new Map() { MapId = 2, Name = "Map 2", Type = "Type 2" },
            new Map() { MapId = 3, Name = "Map 3", Type = "Type 3" },
        });


        modelBuilder.Entity<Player>().HasData(new List<Player>()
        {
            new Player()
                { PlayerId = 1, FirstName = "John", LastName = "Doe", BirthDate = DateTime.Parse("1980-01-01") },
            new Player()
                { PlayerId = 2, FirstName = "Jane", LastName = "Mama", BirthDate = DateTime.Parse("1964-06-21") },
            new Player()
                { PlayerId = 3, FirstName = "Dodo", LastName = "Tyty", BirthDate = DateTime.Parse("1999-03-09") },
        });


        modelBuilder.Entity<Tournament>().HasData(new List<Tournament>()
        {
            new Tournament(){TournamentId = 1, Name = "Tournament 1", StartDate = DateTime.Parse("2021-01-01"), EndDate = DateTime.Parse("2021-03-31")},
            new Tournament(){TournamentId = 2, Name = "Tournament 2", StartDate = DateTime.Parse("2022-01-01"), EndDate = DateTime.Parse("2022-03-31")},
            new Tournament(){TournamentId = 3, Name = "Tournament 3", StartDate = DateTime.Parse("2023-01-01"), EndDate = DateTime.Parse("2023-03-31")},
        });



        modelBuilder.Entity<Match>().HasData(new List<Match>()
        {
            new Match(){MatchId = 1, TournamentId = 1, MapId = 1, MatchDate = DateTime.Parse("2021-01-01"), Team1Score = 1, Team2Score = 2, BestRating = null},
            new Match(){MatchId = 2, TournamentId = 2, MapId = 2, MatchDate = DateTime.Parse("2022-01-01"), Team1Score = 2, Team2Score = 1, BestRating = null},
            new Match(){MatchId = 3, TournamentId = 3, MapId = 3, MatchDate = DateTime.Parse("2023-01-01"), Team1Score = 3, Team2Score = 0, BestRating = null},
        });



        modelBuilder.Entity<PlayerMatch>().HasData(new List<PlayerMatch>()
        {
            new PlayerMatch(){PlayerId = 1, MatchId = 1, Rating = 4.7, MVPs = 1},
            new PlayerMatch(){PlayerId = 2, MatchId = 2, Rating = 3.8, MVPs = 2},
            new PlayerMatch(){PlayerId = 3, MatchId = 3, Rating = 1.6, MVPs = 3}
        });

    base.OnModelCreating(modelBuilder);
    }
}