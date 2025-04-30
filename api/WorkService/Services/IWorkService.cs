using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkService.DTOs;

namespace WorkService.Services
{
    public interface IWorkService
    {
        Task<IEnumerable<WorkReadDto>> GetAllWorksAsync();
        Task<WorkReadDto> GetWorkByIdAsync(Guid id);
        Task<WorkReadDto> CreateWorkAsync(WorkCreateDto workCreateDto);
        Task<WorkReadDto> UpdateWorkAsync(Guid id, WorkUpdateDto workUpdateDto);
        Task<bool> DeleteWorkAsync(Guid id);
    }
}