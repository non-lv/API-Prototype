using API_Prototype.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Prototype.Database
{
    public class Context(DbContextOptions<Context> options) : DbContext(options)
    {
        public virtual DbSet<MessageThread> Threads { get; set; }
        public virtual DbSet<Entry> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageThread>()
                .HasMany(x => x.Entries)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
