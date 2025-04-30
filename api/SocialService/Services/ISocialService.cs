using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialService.DTOs;

namespace SocialService.Services
{
    public interface ISocialService
    {
        Task<IEnumerable<SocialReadDto>> GetAllSocialsAsync();
        Task<SocialReadDto> GetSocialByIdAsync(Guid id);
        Task<SocialReadDto> CreateSocialAsync(SocialCreateDto socialCreateDto);
        Task<SocialReadDto> UpdateSocialAsync(Guid id, SocialUpdateDto socialUpdateDto);
        Task<bool> DeleteSocialAsync(Guid id);
    }
}