using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ReversiMvcApp.Models
{
    public class ReversiDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public ReversiDbContext(DbContextOptions<ReversiDbContext> options)
            : base(options)
        {

        }
    }
}
