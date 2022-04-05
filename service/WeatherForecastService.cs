using Microsoft.EntityFrameworkCore;
using Modal;
using repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class WeatherForecastService
    {
        private IDbContextFactory<nintensdbcontext> contextFactory;

        public WeatherForecastService(IDbContextFactory<nintensdbcontext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        //get all weatherforecastinfo
        public async Task<IEnumerable<WeatherForecast>> getWeatherForecasts()
        {
            using var context = contextFactory.CreateDbContext();
            var listProcessUser = await context.WeatherForecastDataSet.ToListAsync();
            return listProcessUser;
        }
    }
}
