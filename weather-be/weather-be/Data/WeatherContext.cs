using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using weather_be.Data.Entities;

namespace weather_be.Data
{
    public class WeatherContext : DbContext
    {
        public DbSet<UserPlace> userPlaces { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=weather;Username=postgres;Password=123");
        }
    }
}
