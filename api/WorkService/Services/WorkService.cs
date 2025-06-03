using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkService.DTOs;
using WorkService.Models;
using WorkService.Data;
using WorkService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WorkService.Services
{
    public class WorkService : IWorkService
    {
        private readonly WorkDbContext _context;

        public WorkService(WorkDbContext context)
        {
            _context = context;
        }

        // Get all works
        public async Task<IEnumerable<WorkReadDto>> GetAllWorksAsync()
        {
            var works = await _context.Works.ToListAsync();
            return works.Select(works => new WorkReadDto
            {
                Id = works.Id,
                JobTitle = works.JobTitle,
                CompanyName = works.CompanyName,
                LogoUrl = works.LogoUrl,
                Description = works.Description,
                Responsibilities = works.Responsibilities,
                Tags = works.Tags,
                Location = works.Location,
                StartDate = works.StartDate,
                EndDate = works.EndDate
            });
        }

        // Get a single work by its ID
        public async Task<WorkReadDto> GetWorkByIdAsync(Guid id)
        {
            var work = await _context.Works.FindAsync(id);
            if (work == null) return null;

            return new WorkReadDto
            {
                Id = work.Id,
                JobTitle = work.JobTitle,
                CompanyName = work.CompanyName,
                LogoUrl = work.LogoUrl,
                Description = work.Description,
                Responsibilities = work.Responsibilities,
                Tags = work.Tags,
                Location = work.Location,
                StartDate = work.StartDate,
                EndDate = work.EndDate
            };
        }
        // Create a new work
        public async Task<WorkReadDto> CreateWorkAsync(WorkCreateDto workCreateDto)
        {
            var work = new WorkModel
            {
                Id = Guid.NewGuid(),
                CompanyName = workCreateDto.CompanyName,
                JobTitle = workCreateDto.JobTitle,
                LogoUrl = workCreateDto.LogoUrl,
                Description = workCreateDto.Description,
                Responsibilities = workCreateDto.Responsibilities,
                Tags = workCreateDto.Tags,
                Location = workCreateDto.Location,
                StartDate = workCreateDto.StartDate,
                EndDate = workCreateDto.EndDate
            };

            _context.Works.Add(work);
            await _context.SaveChangesAsync();

            return new WorkReadDto
            {
                Id = work.Id,
                CompanyName = work.CompanyName,
                JobTitle = work.JobTitle,
                LogoUrl = work.LogoUrl,
                Description = work.Description,
                Responsibilities = work.Responsibilities,
                Tags = work.Tags,
                Location = work.Location,
                StartDate = work.StartDate,
                EndDate = work.EndDate
            };
        }

        // Update an existing work
        public async Task<WorkReadDto> UpdateWorkAsync(Guid id, WorkUpdateDto workUpdateDto)
        {
            var work = await _context.Works.FindAsync(id);
            if (work == null) return null; // If work not found, return null

            // Update the properties with the new values
            work.CompanyName = workUpdateDto.CompanyName;
            work.JobTitle = workUpdateDto.JobTitle;
            work.LogoUrl = workUpdateDto.LogoUrl;
            work.Description = workUpdateDto.Description;
            work.Responsibilities = workUpdateDto.Responsibilities;
            work.Tags = workUpdateDto.Tags;
            work.Location = workUpdateDto.Location;
            work.StartDate = workUpdateDto.StartDate;
            work.EndDate = workUpdateDto.EndDate;

            // Save the updated work to the database
            _context.Works.Update(work);
            await _context.SaveChangesAsync();

            // Return the updated work as a WorkReadDto
            return new WorkReadDto
            {
                Id = work.Id,
                CompanyName = work.CompanyName,
                JobTitle = work.JobTitle,
                LogoUrl = work.LogoUrl,
                Description = work.Description,
                Responsibilities = work.Responsibilities,
                Tags = work.Tags,
                Location = work.Location,
                StartDate = work.StartDate,
                EndDate = work.EndDate
            };
        }

        // Delete a work by its ID
        public async Task<bool> DeleteWorkAsync(Guid id)
        {
            var work = await _context.Works.FindAsync(id);
            if (work == null) return false;

            _context.Works.Remove(work);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}