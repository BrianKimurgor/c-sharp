using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkService.DTOs;
using WorkService.Models;
using WorkService.Services;
using WorkService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WorkService.Controllers
{
    [Route("api/work")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly IWorkService _workService;

        public WorkController(IWorkService workService)
        {
            _workService = workService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorks()
        {
            var works = await _workService.GetAllWorksAsync();
            return Ok(works);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkById(Guid id)
        {
            var work = await _workService.GetWorkByIdAsync(id);
            if (work == null)
            {
                return NotFound();
            }
            return Ok(work);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWork([FromBody] WorkCreateDto workDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdWork = await _workService.CreateWorkAsync(workDto);
            return CreatedAtAction(nameof(GetWorkById), new { id = createdWork.Id }, createdWork);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWork(Guid id, [FromBody] WorkUpdateDto workDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedWork = await _workService.UpdateWorkAsync(id, workDto);
            if (updatedWork == null)
            {
                return NotFound();
            }
            return Ok(updatedWork);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWork(Guid id)
        {
            var result = await _workService.DeleteWorkAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}