using Microsoft.EntityFrameworkCore;
using weather_be.Data.Entities;

namespace weather_be.Data;

public class WeatherContext : DbContext
{
    public DbSet<UserPlace> userPlaces { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        optionsBuilder.UseNpgsql(connectionString);
    }
}