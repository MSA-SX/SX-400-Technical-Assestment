using Microsoft.EntityFrameworkCore;
using SX400.Api.Data;
using SX400.Api.Models;

namespace SX400.Api.Services;

public interface IWeatherService
{
    Task<IEnumerable<WeatherForecast>> GetAllAsync();
    Task<WeatherForecast?> GetByIdAsync(int id);
    Task<WeatherForecast> CreateAsync(WeatherForecast forecast);
    Task<bool> UpdateAsync(int id, WeatherForecast forecast);
    Task<bool> DeleteAsync(int id);
}

public class WeatherService : IWeatherService
{
    private readonly AppDbContext _context;

    public WeatherService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
    {
        return await _context.WeatherForecasts
            .OrderByDescending(w => w.Date)
            .ToListAsync();
    }

    public async Task<WeatherForecast?> GetByIdAsync(int id)
    {
        return await _context.WeatherForecasts.FindAsync(id);
    }

    public async Task<WeatherForecast> CreateAsync(WeatherForecast forecast)
    {
        _context.WeatherForecasts.Add(forecast);
        await _context.SaveChangesAsync();
        return forecast;
    }

    public async Task<bool> UpdateAsync(int id, WeatherForecast forecast)
    {
        var existing = await _context.WeatherForecasts.FindAsync(id);
        if (existing is null) return false;

        existing.Date = forecast.Date;
        existing.TemperatureC = forecast.TemperatureC;
        existing.Summary = forecast.Summary;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.WeatherForecasts.FindAsync(id);
        if (existing is null) return false;

        _context.WeatherForecasts.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
