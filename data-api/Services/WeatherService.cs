using data_api.Models;

namespace data_api.Services
{
    public class WeatherService
    {
        private static readonly string[] summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecast GetWeather() {
            return new WeatherForecast(
                DateOnly.FromDateTime(DateTime.Now.AddDays(0)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            );
            
        }

        public IEnumerable<WeatherForecast> GetWeatherForecasts()
        {
            return Enumerable.Range(1, 5).Select(index => 
                new WeatherForecast
                {
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                }).ToArray();
        }
    }
}