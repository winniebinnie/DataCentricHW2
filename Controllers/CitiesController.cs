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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
namespace WeatherServer.Controllers
{
    public class CitiesController : CitiesBaseController
    {
        public CitiesController(WeatherServerContext context, IConfiguration configuration) : base(context, configuration)
        {
            
        }

        // GET: Cities
        public async Task<IActionResult> Index(string SearchString)
        {
            if (_context.Cities == null)
            {
                return Problem("No cities in database"); 
            }

            var cityList = from c in _context.Cities select c;
            if (!String.IsNullOrEmpty(SearchString))
            {
                cityList = cityList.Where(s => s.Name!.ToUpper().Contains(SearchString!.ToUpper()));

            }
            return View(await cityList.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            city = await GetUpdatedCityFromApi(city);
            return View(city);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Name)
        {
            if (String.IsNullOrEmpty(Name))
            {
                return BadRequest("Name cannot be empty");
            }
            City city = null;
            try {
                city = await apiService.GetCityAsync(Name);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            if (city == null) {
                return BadRequest("Can't find the city.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}