using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.DTOs;

namespace ProfileService.Services
{
    public interface IProfileService
    {
        Task<IEnumerable<ProfileReadDto>> GetAllProfilesAsync();
        Task<ProfileReadDto?> GetProfileByIdAsync(int id);
        Task<ProfileReadDto> CreateProfileAsync(ProfileCreateDto profileCreateDto);
        Task<ProfileReadDto> UpdateProfileAsync(int id, ProfileUpdateDto profileUpdateDto);
        Task<bool> DeleteProfileAsync(int id);
        Task<bool> ProfileExistsAsync(int id);
        Task<bool> ProfileExistsByNameAsync(string name);
        Task<bool> ProfileExistsByCompanyAsync(string company);
        Task<IEnumerable<ProfileReadDto>> GetProfilesByRoleAsync(string role);
        Task<IEnumerable<ProfileReadDto>> GetProfilesByCompanyAsync(string company);
        Task<IEnumerable<ProfileReadDto>> GetProfilesByNameAsync(string name);
    }
}