using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectService.DTOs;
using ProjectService.Models;

namespace ProjectService.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectReadDto>> GetAllProjectsAsync();
        Task<ProjectReadDto?> GetProjectByIdAsync(Guid id);
        Task<ProjectCreateDto> CreateProjectAsync(ProjectCreateDto projectCreateDto);
        Task<ProjectUpdateDto?> UpdateProjectAsync(Guid id, ProjectUpdateDto projectUpdateDto);
        Task<bool> DeleteProjectAsync(Guid id);
        Task<IEnumerable<ProjectReadDto>> GetProjectsByTagAsync(string tag);

    }
}