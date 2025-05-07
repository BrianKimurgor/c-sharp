using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectService.DTOs;
using ProjectService.Models;
using ProjectService.Data;
using Microsoft.EntityFrameworkCore;

namespace ProjectService.Services
{
    public class ProjectService : IProjectService

    {
        private readonly ProjectDbContext _context;

        public ProjectService(ProjectDbContext context)
        {
            _context = context;
        }

        // Get all projects 
        public async Task<IEnumerable<ProjectReadDto>> GetAllProjectsAsync()
        {
            var projects = await _context.Projects.ToListAsync();
            return projects.Select(p => new ProjectReadDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Tags = p.Tags,
                GitHubUrl = p.GitHubUrl,
                LiveDemoUrl = p.LiveDemoUrl,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            });
        }

        // Get a single project by its ID
        public async Task<ProjectReadDto?> GetProjectByIdAsync(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return null;

            return new ProjectReadDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                ImageUrl = project.ImageUrl,
                Tags = project.Tags,
                GitHubUrl = project.GitHubUrl,
                LiveDemoUrl = project.LiveDemoUrl,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt
            };
        }

        // Create a new project
        public async Task<ProjectReadDto> CreateProjectAsync(ProjectCreateDto projectCreateDto)
        {
            var project = new ProjectModel
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

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return new ProjectReadDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                ImageUrl = project.ImageUrl,
                Tags = project.Tags,
                GitHubUrl = project.GitHubUrl,
                LiveDemoUrl = project.LiveDemoUrl,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt
            };
        }

        // Update an existing project
        public async Task<ProjectReadDto> UpdateProjectAsync(Guid id, ProjectUpdateDto projectUpdateDto)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return null;

            project.Title = projectUpdateDto.Title;
            project.Description = projectUpdateDto.Description;
            project.ImageUrl = projectUpdateDto.ImageUrl;
            project.Tags = projectUpdateDto.Tags;
            project.GitHubUrl = projectUpdateDto.GitHubUrl;
            project.LiveDemoUrl = projectUpdateDto.LiveDemoUrl;
            project.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ProjectReadDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                ImageUrl = project.ImageUrl,
                Tags = project.Tags,
                GitHubUrl = project.GitHubUrl,
                LiveDemoUrl = project.LiveDemoUrl,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt
            };
        }
        // Delete a project by its ID
        public async Task<bool> DeleteProjectAsync(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true; // Project deleted successfully    
        }
        // Get projects by tag
        public async Task<IEnumerable<ProjectReadDto>> GetProjectsByTagAsync(string tag)
        {
            var projects = await _context.Projects
                .Where(p => p.Tags.Contains(tag))
                .ToListAsync();

            return projects.Select(p => new ProjectReadDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Tags = p.Tags,
                GitHubUrl = p.GitHubUrl,
                LiveDemoUrl = p.LiveDemoUrl,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            });
        }
    }
}