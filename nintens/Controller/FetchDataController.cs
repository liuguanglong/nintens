using Microsoft.AspNetCore.Mvc;
using Modal;
using service;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace nintens.Controller
{
    [Authorize(Roles = "ServiceUser", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
