using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
    public class nintensdbcontext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecastDataSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public nintensdbcontext(DbContextOptions<nintensdbcontext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map entities to tables
            modelBuilder.Entity<WeatherForecast>().ToTable("weatherforecast");

            // Configure Primary Keys  
            modelBuilder.Entity<WeatherForecast>().HasKey(ug => ug.Id);

            // Configure columns  
            modelBuilder.Entity<WeatherForecast>().Property(u => u.Id).HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<WeatherForecast>().Property(u => u.TemperatureC).HasColumnType("int").IsRequired();
            modelBuilder.Entity<WeatherForecast>().Property(u => u.Date).HasColumnType("timestamp").IsRequired();
            modelBuilder.Entity<WeatherForecast>().Property(u => u.Summary).HasColumnType("nvarchar(45)").IsRequired(false);

        }
    }
}
