using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.DTOs;
using ProfileService.Models;

namespace ProfileService.Repositories
{
    public interface IprofileRepository
    {
        Task<IEnumerable<ProfileReadDto>> GetAllProfilesAsync();
        Task<ProfileReadDto?> GetProfileByIdAsync(int id);
        Task<ProfileCreateDto> CreateProfileAsync(ProfileCreateDto profile);
        Task<ProfileUpdateDto> UpdateProfileAsync(int id, ProfileUpdateDto profileUpdateDto);

        Task<bool> DeleteProfileAsync(int id);
    }
}