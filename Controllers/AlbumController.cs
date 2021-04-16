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
    public class AlbumController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public AlbumController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;

        }


        // POST api/<AlbumController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Album album)
        {
            await _dbContext.albums.AddAsync(album);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAlbums()
        {
            var albums = await (from album in _dbContext.albums
                                 select new
                                 {
                                     Id = album.Id,
                                     name = album.Name
                                 }).ToListAsync();
            return Ok(albums);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> AlbumDetails(int AlbumId)
        {
            var albumDetails = await _dbContext.albums.Where(a => a.Id == AlbumId).Include(a => a.Songs).ToListAsync();
            return Ok(albumDetails);
        }
    }
}
