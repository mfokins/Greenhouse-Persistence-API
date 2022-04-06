using Microsoft.EntityFrameworkCore;
using Models;

namespace DataServer.Database;

public class DataDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TemperatureMeasurement> TemperatureMeasurements { get; set; }
    public DbSet<HumidityMeasurement> HumidityMeasurements { get; set; }
    public DbSet<DioxideCarbonMeasurement> DioxideCarbonMeasurements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer($"Data source={Variables.DatabaseUri}");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}