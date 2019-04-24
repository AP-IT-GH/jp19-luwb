using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Models
{
    public class DatabaseContext : DbContext
    {
        // 83
        // 48
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        //public DbSet<Coordinate> Coordinates { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Anchor> Anchors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Demo> Demos { get; set; }
    }      
}
