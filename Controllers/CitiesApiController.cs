using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherServer.Models;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
namespace WeatherServer.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CitiesApiController : CitiesBaseController
    {
        public CitiesApiController(WeatherServerContext context, IConfiguration configuration) : base(context, configuration)
        {
            
        }

        // GET: api/CitiesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return await _context.Cities.ToListAsync();
        }

        // GET: api/CitiesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            // YOUR CODE HERE
            City city = new();
            city.Name = "Chiang Mai";
            city.Temperature = 0.0f;
            city.WindDirection = "W";
            city.WindSpeed = 0.0f;
            city.Precipitation = 0.0f;
            city.Condition = "Mock";
            city.UV = 0.0f;
            city.LastUpdated = DateTime.Now;
            return city;
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}
