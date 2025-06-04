using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.DTOs;
using ProfileService.Models;
using ProfileService.Data;
using Microsoft.EntityFrameworkCore;

namespace ProfileService.Repositories
{
    public class ProfileRepository : IprofileRepository
    {
        private readonly List<ProfileReadDto> _profiles = new List<ProfileReadDto>();
        public Task<IEnumerable<ProfileReadDto>> GetAllProfilesAsync()
        {
            return Task.FromResult(_profiles.AsEnumerable());
        }

        public Task<ProfileReadDto?> GetProfileByIdAsync(int id)
        {
            var profile = _profiles.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(profile); // It can be null, which is expected
        }

        public Task<ProfileCreateDto> CreateProfileAsync(ProfileCreateDto profileCreateDto)
        {
            var newProfile = new ProfileReadDto
            {
                Id = _profiles.Count > 0 ? _profiles.Max(p => p.Id) + 1 : 1, // Simple ID generation
                Name = profileCreateDto.Name,
                Role = profileCreateDto.Role,
                Company = profileCreateDto.Company,
                Bio = profileCreateDto.Bio,
                Image = profileCreateDto.Image
            };
            _profiles.Add(newProfile);

            return Task.FromResult(profileCreateDto); // Return the ProfileCreateDto
        }

        public Task<ProfileUpdateDto?> UpdateProfileAsync(int id, ProfileUpdateDto profileUpdateDto)
        {
            var profile = _profiles.FirstOrDefault(p => p.Id == id);
            if (profile != null)
            {
                profile.Name = profileUpdateDto.Name;
                profile.Role = profileUpdateDto.Role;
                profile.Company = profileUpdateDto.Company;
                profile.Bio = profileUpdateDto.Bio;
                profile.Image = profileUpdateDto.Image;
            }
            return Task.FromResult(profileUpdateDto); // Return the ProfileUpdateDto
        }

        public Task<bool> DeleteProfileAsync(int id)
        {
            var profile = _profiles.FirstOrDefault(p => p.Id == id);
            if (profile != null)
            {
                _profiles.Remove(profile);
                return Task.FromResult(true);
            }
            return Task.FromResult(false); // Return false if the profile was not found
        }
    }
}