using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkService.DTOs;
using WorkService.Models;
using WorkService.Data;
using WorkService.Repositories;
using Microsoft.EntityFrameworkCore;


namespace WorkService.Repositories
{
    public class WorkRepository : IWorkRepository
    {
        private readonly WorkDbContext _context;

        public WorkRepository(WorkDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkModel>> GetAllWorksAsync()
        {
            return await _context.Works.ToListAsync();
        }

        public async Task<WorkModel> GetWorkByIdAsync(Guid id)
        {
            return await _context.Works.FindAsync(id);
        }

        public async Task<WorkModel> CreateWorkAsync(WorkModel work)
        {
            await _context.Works.AddAsync(work);
            await _context.SaveChangesAsync();
            return work;
        }

        public async Task<bool> UpdateWorkAsync(Guid id, WorkModel work)
        {
            var existingWork = await GetWorkByIdAsync(id);
            if (existingWork == null) return false;

            existingWork.CompanyName = work.CompanyName;
            existingWork.JobTitle = work.JobTitle;
            existingWork.LogoUrl = work.LogoUrl;
            existingWork.Description = work.Description;
            existingWork.StartDate = work.StartDate;
            existingWork.EndDate = work.EndDate;

            _context.Works.Update(existingWork);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteWorkAsync(Guid id)
        {
            var work = await GetWorkByIdAsync(id);
            if (work == null) return false;

            _context.Works.Remove(work);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}