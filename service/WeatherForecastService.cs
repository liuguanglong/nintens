using Microsoft.EntityFrameworkCore;
using modal;
using Modal;
using repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<Tuple<int, List<OperationLog>>> getLogs(String searchString, String sortLabel, bool desc, int pageSize, int page)
        {
            using var context = contextFactory.CreateDbContext();
            String sql = "";
            if (String.IsNullOrEmpty(searchString))
                sql = String.Format("select * from logs", searchString);
            else
                sql = String.Format("select * from logs where eventid like '%{0}%' or message like '%{0}%' or user like '%{0}%' or ip like '%{0}%' or exception like '%{0}%'", searchString);

            if (String.IsNullOrEmpty(sortLabel))
            {
                sortLabel = "Id";
                desc = true;
            }

            sql = sql + " order by " + sortLabel;
            if (desc)
                sql = sql + " desc";

            var total = context.operationLogs.FromSqlRaw(sql).Count();
            var list = await context.operationLogs.FromSqlRaw(sql).Skip(page * pageSize).Take(pageSize).ToListAsync();

            return Tuple.Create(total, list);
        }
    }
}
