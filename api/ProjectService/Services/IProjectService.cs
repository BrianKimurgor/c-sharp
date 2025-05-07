using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectService.DTOs;

namespace ProjectService.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectReadDto>> GetAllProjectsAsync();
        Task<ProjectReadDto?> GetProjectByIdAsync(Guid id);
        Task<ProjectReadDto> CreateProjectAsync(ProjectCreateDto projectCreateDto); // Should return ProjectReadDto
        Task<ProjectReadDto?> UpdateProjectAsync(Guid id, ProjectUpdateDto projectUpdateDto); // Should return ProjectReadDto
        Task<bool> DeleteProjectAsync(Guid id);
        Task<IEnumerable<ProjectReadDto>> GetProjectsByTagAsync(string tag);
    }
}