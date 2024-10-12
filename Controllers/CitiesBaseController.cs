
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeatherServer.Models;
using WeatherServer.Helpers;

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CA2208 // Instantiate argument exceptions correctly

namespace WeatherServer.Controllers
{
	public class CitiesBaseController : Controller
	{
		protected readonly WeatherServerContext _context;
		protected WeatherAPI apiService;
		protected int cacheLifeMinute;

		public CitiesBaseController(WeatherServerContext context, IConfiguration configuration)
		{
			_context = context;
			apiService = new(configuration);
			cacheLifeMinute = Int32.Parse(configuration["CacheLifeMinute"]);
			}

		protected async Task<City> GetUpdatedCityFromApi(City city)
		{
			ArgumentNullException.ThrowIfNull(city);
			if (DateTime.Now.Subtract(city.LastUpdated).TotalMinutes > cacheLifeMinute)
			{
				try
				{
					City updatedCity = await apiService.GetCityAsync(city.Name);
					if (updatedCity != null)
					{
						city.Temperature = updatedCity.Temperature;
						city.LastUpdated = updatedCity.LastUpdated;
						city.Condition = updatedCity.Condition;
						city.WindSpeed = updatedCity.WindSpeed;
						city.WindDirection = updatedCity.WindDirection;
						city.Humidity = updatedCity.Humidity;
						city.Precipitation = updatedCity.Precipitation;
						city.UV = updatedCity.UV;
						await _context.SaveChangesAsync();
					}
				} 
				catch (Exception ex) 
				{
				}
			}
			return city;
		}
	}
}