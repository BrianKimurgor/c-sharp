using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectService.DTOs;
using ProjectService.Models;
using ProjectService.Services;
using ProjectService.Repositories;

namespace ProjectService.Controllers
{

    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectCreateDto projectCreateDto)
        {
            var createdProject = await _projectService.CreateProjectAsync(projectCreateDto);
            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectUpdateDto projectUpdateDto)
        {
            var updatedProject = await _projectService.UpdateProjectAsync(id, projectUpdateDto);
            if (updatedProject == null) return NotFound();
            return Ok(updatedProject);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("tag/{tag}")]
        public async Task<IActionResult> GetProjectsByTag(string tag)
        {
            var projects = await _projectService.GetProjectsByTagAsync(tag);
            return Ok(projects);
        }
    }
}