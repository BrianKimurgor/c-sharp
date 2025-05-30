using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EducationService.Models;

namespace EducationService.Data
{
    public class EducationDbContext : DbContext
    {
        public EducationDbContext(DbContextOptions<EducationDbContext> options) : base(options)
        {
        }

        public DbSet<EducationModel> Educations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EducationModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SchoolName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Degree).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FieldOfStudy).IsRequired().HasMaxLength(100);
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();
            });
        }
        
    }
}