using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoVision.Dtos;
using GeoVision.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoVision.Controllers
{
    [ApiController]
    [Route("locations")]
    public class LocationGvtController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LocationGvtController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Create a new location.
        /// </summary>
        /// <param name="req">Location details.</param>
        /// <returns>The newly created location.</returns>


        [HttpPost]
        public async Task<ActionResult<LocationGvt>> CreateLocation(CreateLocationDto req)
        {
            try
            {
                if (req.LAT < -90 || req.LAT > 90 || req.LNG < -180 || req.LNG > 180)
                {
                    return BadRequest("Invalid coordinates. Latitude should be between -90 and 90. Longitude should be between -180 and 180.");
                }

                var newLocation = new LocationGvt
                {
                    Name = req.Name,
                    Notes = req.Notes,
                    LAT = req.LAT,
                    LNG = req.LNG
                };
                _context.LOCATIONS_GVT.Add(newLocation);
                await _context.SaveChangesAsync();
                return newLocation;

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<LocationGvt>>> GetAllLocations()
        {
            return await _context.LOCATIONS_GVT.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationGvt>> GetLocation(int id)
        {
            var location = await _context.LOCATIONS_GVT.Where(x => x.ID == id).FirstOrDefaultAsync();
            if (location is not null)
                return location;
            return NotFound();
        }

        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<List<LocationGvt>>> GetLocation(string name)
        {
            var locations = await _context.LOCATIONS_GVT.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
            return locations;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<LocationGvt>> DeleteLocation(int id)
        {
            var location = await _context.LOCATIONS_GVT.Where(x => x.ID == id).FirstOrDefaultAsync();
            if (location is null)
                return NotFound("Location not Found");

            _context.LOCATIONS_GVT.Remove(location);
            await _context.SaveChangesAsync();

            return Ok("Location is deleted");
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<LocationGvt>> EditLocation(int id, CreateLocationDto req)
        {
            var location = await _context.LOCATIONS_GVT.Where(x => x.ID == id).FirstOrDefaultAsync();

            if (location is null)
                return NotFound("Location not found");


            location.Name = req.Name;
            location.Notes = req.Notes;
            location.LAT = req.LAT;
            location.LNG = req.LNG;


            await _context.SaveChangesAsync();
            return location;
        }
    }
}