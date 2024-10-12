using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherServer.Models;

namespace WeatherStation.Models
{
    public class WeatherStationContext : DbContext
    {
        public WeatherStationContext (DbContextOptions<WeatherStationContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherServer.Models.City> City { get; set; } = default!;
    }
}
