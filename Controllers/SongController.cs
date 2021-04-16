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
    public class SongController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public SongController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        // POST api/<SongController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Song song)
        {
            song.UploadedDate = DateTime.Now; 
            await _dbContext.songs.AddAsync(song);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllSongs()
        {
            var songs = await (from song in _dbContext.songs
            select new
            {
                Id = song.Id,
                Title = song.Title,
                Duration = song.Duration
            }).ToListAsync();
            return Ok(songs);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> FeauturedSongs()
        {
            var songs = await (from song in _dbContext.songs
                               where song.IsFeatured == true
                               select new
                               {
                                   Id = song.Id,
                                   Title = song.Title,
                                   Duration = song.Duration
                               }).ToListAsync();
            return Ok(songs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> NewSongs()
        {
            var songs = await (from song in _dbContext.songs
                              orderby song.UploadedDate descending
                               select new
                               {
                                   Id = song.Id,
                                   Title = song.Title,
                                   Duration = song.Duration
                               }).Take(3).ToListAsync();
            return Ok(songs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchSongs(String query)
        {
            var songs = await (from song in _dbContext.songs
                               where song.Title.StartsWith(query)
                               select new
                               {
                                   Id = song.Id,
                                   Title = song.Title,
                                   Duration = song.Duration
                               }).Take(3).ToListAsync();
            return Ok(songs);
        }



    }
}
