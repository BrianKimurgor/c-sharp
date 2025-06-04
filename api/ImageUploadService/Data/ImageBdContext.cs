using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ImageUploadService.Models;

namespace ImageUploadService.Data
{
    public class ImageUploadDbContext : DbContext
    {
        public ImageUploadDbContext(DbContextOptions<ImageUploadDbContext> options) : base(options)
        {
        }

        public DbSet<Image> Images { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FileName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Url).IsRequired().HasMaxLength(500);
                entity.Property(e => e.UploadedAt).IsRequired();
            });
        }
    }
}