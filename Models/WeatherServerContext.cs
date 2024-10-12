using Microsoft.EntityFrameworkCore;

namespace WeatherServer.Models;

public class WeatherServerContext : DbContext
{
	public WeatherServerContext(DbContextOptions<WeatherServerContext> options) : base(options) 
	{

	}

	public DbSet<City> Cities { get; set; } = null!;
}