using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BadgeService.Models;

namespace BadgeService.Data
{
    public class BadgeDbContext : DbContext
    {
        public BadgeDbContext(DbContextOptions<BadgeDbContext> options) : base(options)
        {
        }

        public DbSet<Badge> Badges { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Badge entity
            modelBuilder.Entity<Badge>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
                entity.Property(e => e.ImageUrl).IsRequired().HasMaxLength(200);
                entity.Property(e => e.CreatedAt).IsRequired();
            });
        }
    }
}