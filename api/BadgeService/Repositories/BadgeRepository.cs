using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BadgeService.DTOs;
using BadgeService.Models;
using BadgeService.Data;
using Microsoft.EntityFrameworkCore;

namespace BadgeService.Repositories
{
    public class BadgeRepository : IBadgeRepository
    {
        private readonly BadgeDbContext _context;

        public BadgeRepository(BadgeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BadgeResponseDto>> GetAllBadgesAsync()
        {
            return await _context.Badges.Select(b => new BadgeResponseDto
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                CreatedAt = b.CreatedAt
            }).ToListAsync();
        }

        public async Task<BadgeResponseDto> GetBadgeByIdAsync(int id)
        {
            var badge = await _context.Badges.FindAsync(id);
            if (badge == null) return null;

            return new BadgeResponseDto
            {
                Id = badge.Id,
                Name = badge.Name,
                Description = badge.Description,
                ImageUrl = badge.ImageUrl,
                CreatedAt = badge.CreatedAt
            };
        }

        public async Task<BadgeResponseDto> CreateBadgeAsync(BadgeCreateDto badgeCreateDto)
        {
            var badge = new Badge
            {
                Name = badgeCreateDto.Name,
                Description = badgeCreateDto.Description,
                ImageUrl = badgeCreateDto.ImageUrl,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Badges.AddAsync(badge);
            await _context.SaveChangesAsync();

            return new BadgeResponseDto
            {
                Id = badge.Id,
                Name = badge.Name,
                Description = badge.Description,
                ImageUrl = badge.ImageUrl,
                CreatedAt = badge.CreatedAt
            };
        }

        public async Task<BadgeResponseDto> UpdateBadgeAsync(int id, BadgeCreateDto badgeUpdateDto)
        {
            var badge = await _context.Badges.FindAsync(id);
            if (badge == null) return null;

            badge.Name = badgeUpdateDto.Name;
            badge.Description = badgeUpdateDto.Description;
            badge.ImageUrl = badgeUpdateDto.ImageUrl;

            _context.Badges.Update(badge);
            await _context.SaveChangesAsync();

            return new BadgeResponseDto
            {
                Id = badge.Id,
                Name = badge.Name,
                Description = badge.Description,
                ImageUrl = badge.ImageUrl,
                CreatedAt = badge.CreatedAt
            };
        }

        public async Task<bool> DeleteBadgeAsync(int id)
        {
            var badge = await _context.Badges.FindAsync(id);
            if (badge == null) return false;

            _context.Badges.Remove(badge);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}