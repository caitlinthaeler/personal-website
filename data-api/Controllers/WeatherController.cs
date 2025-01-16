using data_api.Services;
using data_api.Models;
using Microsoft.AspNetCore.Mvc;


namespace data_api.Controllers
{
    
    [ApiController]
    [Route("/api/[controller]")] // app.MapControllers() will use this
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService){
            _weatherService = weatherService;
        }

        [HttpGet("/weatherforecast")] // current url must match this to execute service
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            return _weatherService.GetWeatherForecasts();
        }
    }
}