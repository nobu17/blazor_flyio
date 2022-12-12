using System;
using FlyApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace FlyApp.Server.Data
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<WeatherForecast>()
                .HasKey(x => new { x.Id });
        }
    }
}

