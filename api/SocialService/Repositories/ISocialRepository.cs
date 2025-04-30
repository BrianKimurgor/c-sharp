using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialService.DTOs;
using SocialService.Models;

namespace SocialService.Repositories
{
    public interface ISocialRepository
    {
        Task<IEnumerable<SocialModel>> GetAllSocialsAsync();
        Task<SocialModel> GetSocialByIdAsync(Guid id);
        Task<SocialModel> CreateSocialAsync(SocialModel social);
        Task<bool> UpdateSocialAsync(Guid id, SocialModel social);
        Task<bool> DeleteSocialAsync(Guid id);
    }
}