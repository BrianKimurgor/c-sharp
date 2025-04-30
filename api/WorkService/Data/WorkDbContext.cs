using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkService.Models;
using Microsoft.EntityFrameworkCore;

namespace WorkService.Data
{
    public class WorkDbContext : DbContext
    {
        public WorkDbContext(DbContextOptions<WorkDbContext> options) : base(options)
        {
        }

        public DbSet<WorkModel> Works { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Work entity
            modelBuilder.Entity<WorkModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.JobTitle).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LogoUrl).HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired(false);
            });
        }
    }
}