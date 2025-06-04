using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.DTOs;
using ProfileService.Models;
using ProfileService.Data;
using Microsoft.EntityFrameworkCore;

namespace ProfileService.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ProfileDbContext _context;
        public ProfileService(ProfileDbContext context)
        {
            _context = context;
        }

        // Get all profiles
        public async Task<IEnumerable<ProfileReadDto>> GetAllProfilesAsync()
        {
            var profiles = await _context.Profiles.ToListAsync();
            return profiles.Select(p => new ProfileReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Role = p.Role,
                Company = p.Company,
                Bio = p.Bio,
                Image = p.Image
            });
        }

        // Get a single profile by its ID
        public async Task<ProfileReadDto?> GetProfileByIdAsync(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null) return null;

            return new ProfileReadDto
            {
                Id = profile.Id,
                Name = profile.Name,
                Role = profile.Role,
                Company = profile.Company,
                Bio = profile.Bio,
                Image = profile.Image
            };
        }

        // Create a new profile
        public async Task<ProfileReadDto> CreateProfileAsync(ProfileCreateDto profileCreateDto)
        {
            var profile = new ProfileModel
            {
                Name = profileCreateDto.Name,
                Role = profileCreateDto.Role,
                Company = profileCreateDto.Company,
                Bio = profileCreateDto.Bio,
                Image = profileCreateDto.Image
            };

            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();

            return new ProfileReadDto
            {
                Id = profile.Id,
                Name = profile.Name,
                Role = profile.Role,
                Company = profile.Company,
                Bio = profile.Bio,
                Image = profile.Image
            };
        }

        // Update an existing profile
        public async Task<ProfileReadDto?> UpdateProfileAsync(int id, ProfileUpdateDto profileUpdateDto)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null) return null;

            profile.Name = profileUpdateDto.Name;
            profile.Role = profileUpdateDto.Role;
            profile.Company = profileUpdateDto.Company;
            profile.Bio = profileUpdateDto.Bio;
            profile.Image = profileUpdateDto.Image;

            _context.Profiles.Update(profile);
            await _context.SaveChangesAsync();

            return new ProfileReadDto
            {
                Id = profile.Id,
                Name = profile.Name,
                Role = profile.Role,
                Company = profile.Company,
                Bio = profile.Bio,
                Image = profile.Image
            };
        }

        // Delete a profile
        public async Task<bool> DeleteProfileAsync(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null) return false;

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
            return true;
        }

        // Additional methods can be added here as needed
        // For example, methods to search profiles by name, role, etc. can be implemented

        public async Task<bool> ProfileExistsAsync(int id)
        {
            return await _context.Profiles.AnyAsync(p => p.Id == id);
        }
        public async Task<bool> ProfileExistsByNameAsync(string name)
        {
            return await _context.Profiles.AnyAsync(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        public async Task<bool> ProfileExistsByCompanyAsync(string company)
        {
            return await _context.Profiles.AnyAsync(p => p.Company.Equals(company, StringComparison.OrdinalIgnoreCase));
        }
        public async Task<IEnumerable<ProfileReadDto>> GetProfilesByRoleAsync(string role)
        {
            var profiles = await _context.Profiles
                .Where(p => p.Role.Equals(role, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            return profiles.Select(p => new ProfileReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Role = p.Role,
                Company = p.Company,
                Bio = p.Bio,
                Image = p.Image
            });
        }
        public async Task<IEnumerable<ProfileReadDto>> GetProfilesByCompanyAsync(string company)
        {
            var profiles = await _context.Profiles
                .Where(p => p.Company.Equals(company, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            return profiles.Select(p => new ProfileReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Role = p.Role,
                Company = p.Company,
                Bio = p.Bio,
                Image = p.Image
            });
        }

        public async Task<IEnumerable<ProfileReadDto>> GetProfilesByNameAsync(string name)
        {
            var profiles = await _context.Profiles
                .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            return profiles.Select(p => new ProfileReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Role = p.Role,
                Company = p.Company,
                Bio = p.Bio,
                Image = p.Image
            });
        }
    }
}