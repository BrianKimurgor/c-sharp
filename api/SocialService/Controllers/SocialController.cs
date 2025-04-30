using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialService.DTOs;
using SocialService.Models;
using SocialService.Repositories;
using SocialService.Services;

namespace SocialService.Controllers
{
    [Route("api/socials")]
    [ApiController]
    public class SocialController : ControllerBase
    {
        public readonly ISocialService _socialService;

        public SocialController(ISocialService socialService)
        {
            _socialService = socialService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSocials()
        {
            var socials = await _socialService.GetAllSocialsAsync();
            return Ok(socials);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSocialById(Guid id)
        {
            var social = await _socialService.GetSocialByIdAsync(id);
            if (social == null)
            {
                return NotFound();
            }
            return Ok(social);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSocial([FromBody] SocialCreateDto socialDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdSocial = await _socialService.CreateSocialAsync(socialDto);
            return CreatedAtAction(nameof(GetSocialById), new { id = createdSocial.Id }, createdSocial);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSocial(Guid id, [FromBody] SocialUpdateDto socialDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedSocial = await _socialService.UpdateSocialAsync(id, socialDto);
            if (updatedSocial == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocial(Guid id)
        {
            var deleted = await _socialService.DeleteSocialAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}