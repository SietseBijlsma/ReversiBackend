using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReversiRestApi.DAL;
using System.Configuration;

namespace ReversiRestApi.Models.Databse
{
    public class DatabaseContext : DbContext
    {
        public DbSet<GameModel> Games { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
