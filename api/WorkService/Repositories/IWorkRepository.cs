using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkService.DTOs;
using WorkService.Models;

namespace WorkService.Repositories
{
    public interface IWorkRepository
    {
        Task<IEnumerable<WorkModel>> GetAllWorksAsync();
        Task<WorkModel> GetWorkByIdAsync(Guid id);
        Task<WorkModel> CreateWorkAsync(WorkModel work);
        Task<bool> UpdateWorkAsync(Guid id, WorkModel work);
        Task<bool> DeleteWorkAsync(Guid id);
    }
}