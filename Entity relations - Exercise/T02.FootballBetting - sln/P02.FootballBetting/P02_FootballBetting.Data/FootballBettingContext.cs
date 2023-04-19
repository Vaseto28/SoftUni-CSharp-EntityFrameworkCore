using Microsoft.EntityFrameworkCore;
using P02_FootballBetting.Common;
using P02_FootballBetting.Data.Models;

namespace P02_FootballBetting.Data;

public class FootballBettingContext : DbContext
{
    public FootballBettingContext()
    {

    }

    public FootballBettingContext(DbContextOptions options)
        : base(options)
    {

    }

    public DbSet<Bet> Bets { get; set; }

    public DbSet<Color> Colors { get; set; }

    public DbSet<Country> Countries { get; set; }

    public DbSet<Game> Games { get; set; }

    public DbSet<Player> Players { get; set; }

    public DbSet<PlayerStatistic> PlayersStatistics { get; set; }

    public DbSet<Position> Positions { get; set; }

    public DbSet<Team> Teams { get; set; }

    public DbSet<Town> Towns { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(Config.ConnectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerStatistic>(entity =>
        {
            entity.HasKey(x => new { x.GameId, x.PlayerId });
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasOne(x => x.PrimaryKitColor)
                .WithMany(x => x.PrimaryKitTeams)
                .HasForeignKey(x => x.PrimaryKitColorId);

            entity.HasOne(x => x.SecondaryKitColor)
                .WithMany(x => x.SecondaryKitTeams)
                .HasForeignKey(x => x.SecondaryKitColorId);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasOne(x => x.HomeTeam)
                .WithMany(x => x.HomeGames)
                .HasForeignKey(x => x.HomeTeamId);

            entity.HasOne(x => x.AwayTeam)
                .WithMany(x => x.AwayGames)
                .HasForeignKey(x => x.AwayTeamId);
        });

        base.OnModelCreating(modelBuilder);
    }
}