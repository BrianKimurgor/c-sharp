using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialService.Models;

namespace SocialService.Data
{
    public class SocialDbContext : DbContext
    {
        public SocialDbContext(DbContextOptions<SocialDbContext> options) : base(options)
        {
        }

        public DbSet<SocialModel> Socials { get; set; } = null!; // Initialize to null for EF Core compatibility

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Social entity
            modelBuilder.Entity<SocialModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Platform).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Url).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Icon).HasMaxLength(200); // Optional: for frontend icon reference
            });
        }
    }
}