using Microsoft.AspNetCore.Mvc;
using SX400.Api.Models;
using SX400.Api.Services;

namespace SX400.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    /// <summary>
    /// Get all weather forecasts.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetAll()
    {
        var forecasts = await _weatherService.GetAllAsync();
        return Ok(forecasts);
    }

    /// <summary>
    /// Get a specific weather forecast by ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<WeatherForecast>> GetById(int id)
    {
        var forecast = await _weatherService.GetByIdAsync(id);
        if (forecast is null) return NotFound();
        return Ok(forecast);
    }

    /// <summary>
    /// Create a new weather forecast.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<WeatherForecast>> Create([FromBody] WeatherForecast forecast)
    {
        var created = await _weatherService.CreateAsync(forecast);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Update an existing weather forecast.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] WeatherForecast forecast)
    {
        var updated = await _weatherService.UpdateAsync(id, forecast);
        if (!updated) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Delete a weather forecast.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _weatherService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
