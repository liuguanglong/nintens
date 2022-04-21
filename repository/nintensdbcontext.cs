using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using modal;
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
        public DbSet<OperationLog> operationLogs { get; set; }

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
            modelBuilder.Entity<OperationLog>().ToTable("logs");

            // Configure Primary Keys  
            modelBuilder.Entity<WeatherForecast>().HasKey(ug => ug.Id);
            modelBuilder.Entity<OperationLog>().HasKey(ug => ug.Id);

            // Configure columns  
            modelBuilder.Entity<WeatherForecast>().Property(u => u.Id).HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<WeatherForecast>().Property(u => u.TemperatureC).HasColumnType("int").IsRequired();
            modelBuilder.Entity<WeatherForecast>().Property(u => u.Date).HasColumnType("timestamp").IsRequired();
            modelBuilder.Entity<WeatherForecast>().Property(u => u.Summary).HasColumnType("nvarchar(45)").IsRequired(false);
            

            modelBuilder.Entity<OperationLog>().Property(u => u.Id).HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<OperationLog>().Property(u => u.EventInfo).HasColumnName("EventId").HasColumnType("nvarchar(45)").IsRequired(false);
            modelBuilder.Entity<OperationLog>().Property(u => u.Timestamp).HasColumnType("timestamp").IsRequired(false);
            modelBuilder.Entity<OperationLog>().Property(u => u.Message).HasColumnType("Text").IsRequired(false);
            modelBuilder.Entity<OperationLog>().Property(u => u.User).HasColumnType("nvarchar(45)").IsRequired(false);
            modelBuilder.Entity<OperationLog>().Property(u => u.Ip).HasColumnType("nvarchar(45)").IsRequired(false);
            modelBuilder.Entity<OperationLog>().Property(u => u.Level).HasColumnType("nvarchar(15)").IsRequired(false);
            modelBuilder.Entity<OperationLog>().Property(u => u.Exception).HasColumnType("Text").IsRequired(false);
            modelBuilder.Entity<OperationLog>().Property(u => u.Properties).HasColumnType("Text").IsRequired(false);
        }
    }
}
