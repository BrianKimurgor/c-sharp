using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectService.DTOs;
using ProjectService.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectService.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly List<ProjectReadDto> _projects = new List<ProjectReadDto>();

        public Task<IEnumerable<ProjectReadDto>> GetAllProjectsAsync()
        {
            return Task.FromResult(_projects.AsEnumerable());
        }

        public Task<ProjectReadDto?> GetProjectByIdAsync(Guid id)
        {
            var project = _projects.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(project); // It can be null, which is expected
        }

        public Task<ProjectCreateDto> CreateProjectAsync(ProjectCreateDto projectCreateDto)
        {
            var newProject = new ProjectReadDto
            {
                Id = Guid.NewGuid(),
                Title = projectCreateDto.Title,
                Description = projectCreateDto.Description,
                ImageUrl = projectCreateDto.ImageUrl,
                Tags = projectCreateDto.Tags,
                GitHubUrl = projectCreateDto.GitHubUrl,
                LiveDemoUrl = projectCreateDto.LiveDemoUrl,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _projects.Add(newProject);

            return Task.FromResult(projectCreateDto); // Return the ProjectCreateDto
        }

        public Task<ProjectUpdateDto?> UpdateProjectAsync(Guid id, ProjectUpdateDto projectUpdateDto)
        {
            var project = _projects.FirstOrDefault(p => p.Id == id);
            if (project != null)
            {
                project.Title = projectUpdateDto.Title;
                project.Description = projectUpdateDto.Description;
                project.ImageUrl = projectUpdateDto.ImageUrl;
                project.Tags = projectUpdateDto.Tags;
                project.GitHubUrl = projectUpdateDto.GitHubUrl;
                project.LiveDemoUrl = projectUpdateDto.LiveDemoUrl;
                project.UpdatedAt = DateTime.UtcNow;
            }
            return Task.FromResult(projectUpdateDto); // Return the ProjectUpdateDto
        }

        public Task<bool> DeleteProjectAsync(Guid id)
        {
            var project = _projects.FirstOrDefault(p => p.Id == id);
            if (project != null)
            {
                _projects.Remove(project);
                return Task.FromResult(true); // Project deleted successfully
            }
            return Task.FromResult(false); // Project not found
        }

        public Task<IEnumerable<ProjectReadDto>> GetProjectsByTagAsync(string tag)
        {
            var projectsWithTag = _projects.Where(p => p.Tags.Contains(tag)).ToList();
            return Task.FromResult(projectsWithTag.AsEnumerable());
        }
    }
}