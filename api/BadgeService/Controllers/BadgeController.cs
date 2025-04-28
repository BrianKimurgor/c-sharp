using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BadgeService.DTOs;
using BadgeService.Services;
using BadgeService.Models;
using BadgeService.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace BadgeService.Controllers
{
    [Route("api/badges")]
    [ApiController]

    public class BadgeController : ControllerBase
    {
        private readonly IBadgeService _badgeService;

        public BadgeController(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBadges()
        {
            var badges = await _badgeService.GetAllBadgesAsync();
            return Ok(badges);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBadgeById(int id)
        {
            var badge = await _badgeService.GetBadgeByIdAsync(id);
            if (badge == null) return NotFound();
            return Ok(badge);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBadge([FromBody] BadgeCreateDto badgeCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdBadge = await _badgeService.CreateBadgeAsync(badgeCreateDto);
            return CreatedAtAction(nameof(GetBadgeById), new { id = createdBadge.Id }, createdBadge);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBadge(int id, [FromBody] BadgeCreateDto badgeUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedBadge = await _badgeService.UpdateBadgeAsync(id, badgeUpdateDto);
            if (updatedBadge == null) return NotFound();
            return Ok(updatedBadge);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBadge(int id)
        {
            var deleted = await _badgeService.DeleteBadgeAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}