using Microsoft.EntityFrameworkCore;
using Modal;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace repository.test
{
    public class Tests
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
            var dbcontext = factory.Object.CreateDbContext();
            WeatherForecast info = new WeatherForecast();
            info.Date = DateTime.Now;
            info.Summary = "Warm";
            info.TemperatureC = 5;
            var ret = dbcontext.WeatherForecastDataSet.Add(info);
            dbcontext.SaveChanges();

            var id = ret.Entity.Id;
            WeatherForecast info1 = dbcontext.WeatherForecastDataSet.Where(x => x.Id == id).FirstOrDefault();
            Assert.IsNotNull(info1);
            Assert.AreEqual(info1.Date, info.Date);
        }
    }
}