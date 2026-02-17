using Microsoft.EntityFrameworkCore;
using SX400.Api.Models;

namespace SX400.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed sample data
        modelBuilder.Entity<WeatherForecast>().HasData(
            new WeatherForecast { Id = 1, Date = new DateOnly(2026, 1, 15), TemperatureC = 22, Summary = "Warm" },
            new WeatherForecast { Id = 2, Date = new DateOnly(2026, 1, 16), TemperatureC = -5, Summary = "Freezing" },
            new WeatherForecast { Id = 3, Date = new DateOnly(2026, 1, 17), TemperatureC = 35, Summary = "Hot" },
            new WeatherForecast { Id = 4, Date = new DateOnly(2026, 1, 18), TemperatureC = 10, Summary = "Cool" },
            new WeatherForecast { Id = 5, Date = new DateOnly(2026, 1, 19), TemperatureC = 28, Summary = "Balmy" }
        );
    }
}
