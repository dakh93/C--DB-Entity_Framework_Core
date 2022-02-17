using _05.Entity_Relations_FootballBettingDatabase.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _05.Entity_Relations_FootballBettingDatabase.Data
{
    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext() {}

        public FootballBettingContext(DbContextOptions options)
        : base(options){}

        public DbSet<Country> Countries { get; set; }
        public DbSet<Town> Towns{ get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStatistic> PlayersStatistics { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString); 
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //COUNTRY
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

                entity.HasMany(e => e.Towns)
                .WithOne(t => t.Country)
                .HasForeignKey(e => e.TownId);
            });

            //TOWNS
            modelBuilder.Entity<Town>(entity =>
            {
                entity.HasKey(e => e.TownId);

                entity.Property(e => e.Name)
               .IsRequired()
               .IsUnicode()
               .HasMaxLength(50);

                entity.HasMany(e => e.Teams)
                .WithOne(t => t.Town)
                .HasForeignKey(e => e.TeamId);
            });

            //Color
            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.ColorId);

                entity.Property(e => e.Name)
               .IsRequired()
               .IsUnicode()
               .HasMaxLength(50);

                //entity.HasMany(e => e.PrimaryKitColorTeams)
                //.WithOne(pk => pk.PrimaryKitColor)
                //.HasForeignKey(e => e.PrimaryKitColorId);

                //entity.HasMany(e => e.SecondaryKitColorTeams)
                //.WithOne(pk => pk.SecondaryKitColor)
                //.HasForeignKey(e => e.SecondaryKitColorId);
            });

            //Position
            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.PositionId);

                entity.Property(e => e.Name)
               .IsRequired()
               .IsUnicode()
               .HasMaxLength(50);

                entity.HasMany(e => e.Players)
                .WithOne(p => p.Position)
                .HasForeignKey(e => e.PlayerId);
            });

            //Player
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.PlayerId);

                entity.Property(e => e.Name)
               .IsRequired()
               .IsUnicode()
               .HasMaxLength(50);

                entity.Property(e => e.IsInjured)
                .HasDefaultValue(false);

            });

            //PlayerStatistics
            modelBuilder.Entity<PlayerStatistic>(entity =>
            {
                entity.HasKey(e => new { e.PlayerId, e.GameId });

                entity.HasOne(e => e.Player)
                .WithMany(p => p.PlayersStatistics)
                .HasForeignKey(e =>e.PlayerId);

                entity.HasOne(e => e.Game)
                .WithMany(g => g.PlayersStatistics)
                .HasForeignKey(e => e.GameId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            //Team
            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity.Property(e => e.Budget)
                .HasDefaultValue(0.00)
                .HasColumnType("DECIMAL")
                .HasPrecision(2);

                entity.HasMany(e => e.HomeGames)
                .WithOne(g => g.HomeTeam)
                .HasForeignKey(e => e.GameId);

                entity.HasMany(e => e.AwayGames)
                .WithOne(g => g.AwayTeam)
                .HasForeignKey(e => e.GameId)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(e => e.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(e => e.PlayerId);

                entity.HasOne(e => e.PrimaryKitColor)
                .WithMany(c => c.PrimaryKitColorTeams)
                .HasForeignKey(e => e.PrimaryKitColorId);

                entity.HasOne(e => e.SecondaryKitColor)
                .WithMany(c => c.SecondaryKitColorTeams)
                .HasForeignKey(e => e.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            //Game
            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.GameId);

                entity.Property(e => e.DateTime)
                .IsRequired()
                .HasColumnType("DATETIME2");

                entity.Property(e => e.AwayTeamBetRate)
                .HasDefaultValue(0.00)
                .HasColumnType("DECIMAL")
                .HasPrecision(2);

                entity.Property(e => e.HomeTeamBetRate)
                .HasDefaultValue(0.00)
                .HasColumnType("DECIMAL")
                .HasPrecision(2);

                entity.Property(e => e.DrawBetRate)
                .HasDefaultValue(0.00)
                .HasColumnType("DECIMAL")
                .HasPrecision(2);

                entity.HasMany(e => e.Bets)
                .WithOne(b => b.Game)
                .HasForeignKey(e => e.BetId);

            });

            //Bet
            modelBuilder.Entity<Bet>(entity =>
            {
                entity.HasKey(e => e.BetId);

                entity.Property(e => e.DateTime)
               .IsRequired()
               .HasColumnType("DATETIME2");

                entity.Property(e => e.Amount)
                .HasDefaultValue(0.00)
                .HasColumnType("DECIMAL")
                .HasPrecision(2);

            });

            //User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Balance)
                .HasDefaultValue(0.00)
                .HasColumnType("DECIMAL")
                .HasPrecision(2);

                entity.HasMany(e => e.Bets)
                .WithOne(b => b.User)
                .HasForeignKey(e => e.BetId);

            });
        }
    }
}