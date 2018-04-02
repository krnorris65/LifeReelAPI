using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LifeReelAPI.Models;

namespace LifeReelAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> User {get;set;}
        public DbSet<Event> Event {get;set;}
        public DbSet<Friend> Friend {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //automatically sets the date when a user or friend is created
            modelBuilder.Entity<User>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
            modelBuilder.Entity<Friend>()
                .Property(b => b.Date)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
        }

        
    }
}