using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using repository;
using System;

namespace service.test
{
    public class TestWeatherForecastService
    {
        Mock<IDbContextFactory<nintensdbcontext>> factory;

        [SetUp]
        public void Setup()
        {
            String connString = "server=localhost; userid=pgo;pwd=pgoplus;port=3306;database=nintens;Allow Zero Datetime=true;Convert Zero Datetime=True";
            var optionsBuilder = new DbContextOptionsBuilder<nintensdbcontext>();
            optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));

            factory = new Mock<IDbContextFactory<nintensdbcontext>>();
            factory.Setup(f => f.CreateDbContext())
                .Returns(new nintensdbcontext(optionsBuilder.Options));
        }

        [Test]
        public void Test1()
        {
            WeatherForecastService service = new WeatherForecastService(factory.Object);
            var list = service.getWeatherForecasts().Result;
            Assert.IsNotNull(list);
            foreach (var item in list)
                System.Console.WriteLine($"{item.Id} {item.Date} {item.TemperatureC} {item.TemperatureF} {item.Summary}");
        }
    }
}