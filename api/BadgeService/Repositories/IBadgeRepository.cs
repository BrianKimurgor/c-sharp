using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BadgeService.DTOs;
using BadgeService.Models;

namespace BadgeService.Repositories
{
    public interface IBadgeRepository
    {
        Task<IEnumerable<BadgeResponseDto>> GetAllBadgesAsync();
        Task<BadgeResponseDto> GetBadgeByIdAsync(int id);
        Task<BadgeResponseDto> CreateBadgeAsync(BadgeCreateDto badgeCreateDto);
        Task<BadgeResponseDto> UpdateBadgeAsync(int id, BadgeCreateDto badgeUpdateDto);
        Task<bool> DeleteBadgeAsync(int id);
    }
}