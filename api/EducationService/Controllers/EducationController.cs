using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationService.DTOs;
using EducationService.Models;
using EducationService.Repositories;
using EducationService.Services;
using Microsoft.AspNetCore.Mvc;


namespace EducationService.Controllers
{
    [Route("api/educations")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEducations()
        {
            var educations = await _educationService.GetAllEducationsAsync();
            return Ok(educations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEducationById(Guid id)
        {
            var education = await _educationService.GetEducationByIdAsync(id);
            if (education == null) return NotFound();
            return Ok(education);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEducation([FromBody] EducationCreateDto educationCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdEducation = await _educationService.CreateEducationAsync(educationCreateDto);
            return CreatedAtAction(nameof(GetEducationById), new { id = createdEducation.Id }, createdEducation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEducation(Guid id, [FromBody] EducationUpdateDto educationUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedEducation = await _educationService.UpdateEducationAsync(id, educationUpdateDto);
            if (updatedEducation == null) return NotFound();
            return Ok(updatedEducation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(Guid id)
        {
            var result = await _educationService.DeleteEducationAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        
    }
}