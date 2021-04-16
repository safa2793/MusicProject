using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public ArtistController(ApiDbContext dbContext)
        {
            _dbContext = dbContext; 

        }
        // POST api/<ArtistController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Artist artist)
        {
            await _dbContext.artists.AddAsync(artist);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetArtists ()
        {
            var artists = await ( from artist in _dbContext.artists select new {
                Id = artist.Id,
                name = artist.Name
            }).ToListAsync();

            return Ok(artists);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ArtistDetails (int ArtistId)
        {
            var artistDetails = await _dbContext.artists.Where(a=>a.Id == ArtistId).Include(a => a.Songs).ToListAsync();
            return Ok(artistDetails);
        }
    }
}
