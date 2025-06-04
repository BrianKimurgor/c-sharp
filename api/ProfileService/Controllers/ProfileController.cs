using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.DTOs;
using ProfileService.Models;
using ProfileService.Repositories;
using ProfileService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ProfileService.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var profiles = await _profileService.GetAllProfilesAsync();
            return Ok(profiles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById(int id)
        {
            var profile = await _profileService.GetProfileByIdAsync(id);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] ProfileCreateDto profileCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdProfile = await _profileService.CreateProfileAsync(profileCreateDto);
            return CreatedAtAction(nameof(GetProfileById), new { id = createdProfile.Id }, createdProfile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] ProfileUpdateDto profileUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedProfile = await _profileService.UpdateProfileAsync(id, profileUpdateDto);
            if (updatedProfile == null) return NotFound();
            return Ok(updatedProfile);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var result = await _profileService.DeleteProfileAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("exists/{id}")]
        public async Task<IActionResult> ProfileExists(int id)
        {
            var exists = await _profileService.ProfileExistsAsync(id);
            return Ok(exists);
        }

        [HttpGet("exists/name/{name}")]
        public async Task<IActionResult> ProfileExistsByName(string name)
        {
            var exists = await _profileService.ProfileExistsByNameAsync(name);
            return Ok(exists);
        }

        [HttpGet("exists/company/{company}")]
        public async Task<IActionResult> ProfileExistsByCompany(string company)
        {
            var exists = await _profileService.ProfileExistsByCompanyAsync(company);
            return Ok(exists);
        }

        [HttpGet("role/{role}")]
        public async Task<IActionResult> GetProfilesByRole(string role)
        {
            var profiles = await _profileService.GetProfilesByRoleAsync(role);
            return Ok(profiles);
        }

        
    }
}