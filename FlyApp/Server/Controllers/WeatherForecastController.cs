using Microsoft.AspNetCore.Mvc;
using FlyApp.Shared;
using System;
using FlyApp.Server.Data;

namespace FlyApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly WeatherDbContext _context;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(WeatherDbContext context, ILogger<WeatherForecastController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return _context.WeatherForecasts.ToArray();
    }
}

