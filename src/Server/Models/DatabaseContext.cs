using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Anchor> Anchors { get; set; }
    }
}
