using Microsoft.AspNetCore.Mvc;
using Modal;
using service;

namespace nintens.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FetchDataController : ControllerBase
    {
        WeatherForecastService service;
        public FetchDataController(WeatherForecastService taskService)
        {
            this.service = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetWeatherForecast()
        {
            var forecasts = await service.getWeatherForecasts();
            return Ok(forecasts);
        }
    }
}
