namespace Venato.Service.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController, Route("[controller]")]
internal sealed class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Balmy",
        "Bracing",
        "Chilly",
        "Cool",
        "Freezing",
        "Hot",
        "Mild",
        "Scorching",
        "Sweltering",
        "Warm",
    };

    private readonly ILogger<WeatherForecastController> logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
        => (this.logger) = (logger);

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
