using BadgeService.Data;
using BadgeService.DTOs;
using BadgeService.Models;
using Microsoft.EntityFrameworkCore;

namespace BadgeService.Services
{
    public class BadgeService : IBadgeService
    {
        private readonly BadgeDbContext _context;

        public BadgeService(BadgeDbContext context)
        {
            _context = context;
        }

        // Get all badges from the database
        public async Task<IEnumerable<BadgeResponseDto>> GetAllBadgesAsync()
        {
            return await _context.Badges
                .Select(b => new BadgeResponseDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    CreatedAt = b.CreatedAt
                })
                .ToListAsync();
        }

        // Get a badge by its ID
        public async Task<BadgeResponseDto> GetBadgeByIdAsync(int id)
        {
            var badge = await _context.Badges
                .FirstOrDefaultAsync(b => b.Id == id);

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

        // Create a new badge
        public async Task<BadgeResponseDto> CreateBadgeAsync(BadgeCreateDto badgeCreateDto)
        {
            var newBadge = new Badge
            {
                Name = badgeCreateDto.Name,
                Description = badgeCreateDto.Description,
                ImageUrl = badgeCreateDto.ImageUrl,
                CreatedAt = DateTime.UtcNow
            };

            _context.Badges.Add(newBadge);
            await _context.SaveChangesAsync();

            return new BadgeResponseDto
            {
                Id = newBadge.Id,
                Name = newBadge.Name,
                Description = newBadge.Description,
                ImageUrl = newBadge.ImageUrl,
                CreatedAt = newBadge.CreatedAt
            };
        }

        // Update an existing badge
        public async Task<BadgeResponseDto> UpdateBadgeAsync(int id, BadgeCreateDto badgeUpdateDto)
        {
            var badge = await _context.Badges
                .FirstOrDefaultAsync(b => b.Id == id);

            if (badge == null) return null;

            badge.Name = badgeUpdateDto.Name;
            badge.Description = badgeUpdateDto.Description;
            badge.ImageUrl = badgeUpdateDto.ImageUrl;

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

        // Delete a badge by ID
        public async Task<bool> DeleteBadgeAsync(int id)
        {
            var badge = await _context.Badges
                .FirstOrDefaultAsync(b => b.Id == id);

            if (badge == null) return false;

            _context.Badges.Remove(badge);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
