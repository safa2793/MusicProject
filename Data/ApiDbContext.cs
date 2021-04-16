using Microsoft.EntityFrameworkCore;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options ) : base (options)
        {

        }
        public DbSet<Song> songs { get; set; }
        public DbSet<Artist> artists { get; set; }
        public DbSet<Album> albums { get; set; }

        
    }
}
