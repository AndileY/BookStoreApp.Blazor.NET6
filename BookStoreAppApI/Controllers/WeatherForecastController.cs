using Microsoft.AspNetCore.Mvc;

namespace BookStoreAppApI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // Change the return type to IActionResult instead of IEnumerable<WeatherForecast>
        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            _logger.LogInformation("Make call to Weather Endpoint");

            try
            {
                // Simulating an exception for logging purpose
                throw new Exception("This is our logging text exception");

                return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }).ToArray());
            }
            catch (Exception ex)
            {
                // Log the error with detailed information
                _logger.LogError(ex, "Fatal Error Occurred");

                // Return a custom 500 Internal Server Error response with message
                var errorResponse = new
                {
                    message = "An unexpected error occurred.",
                    details = ex.Message
                };

                // Returning a 500 status code with the error response
                return StatusCode(500, errorResponse);
            }
        }
    }

}
    
