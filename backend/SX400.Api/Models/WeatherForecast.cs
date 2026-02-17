using System.ComponentModel.DataAnnotations;

namespace SX400.Api.Models;

public class WeatherForecast
{
    [Key]
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    [MaxLength(200)]
    public string? Summary { get; set; }
}
