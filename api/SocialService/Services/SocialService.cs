using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialService.DTOs;
using SocialService.Models;
using SocialService.Data;
using SocialService.Repositories;
using Microsoft.EntityFrameworkCore;


namespace SocialService.Services
{
    public class SocialService : ISocialService
    {
        private readonly SocialDbContext _context;
        public SocialService(SocialDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<SocialReadDto>> GetAllSocialsAsync()
        {
            var socials = await _context.Socials.ToListAsync();
            return socials.Select(s => new SocialReadDto
            {
                Id = s.Id,
                Platform = s.Platform,
                Url = s.Url,
                Icon = s.Icon
            });
        }

        public async Task<SocialReadDto> GetSocialByIdAsync(Guid id)
        {
            var social = await _context.Socials.FindAsync(id);
            if (social == null) return null;

            return new SocialReadDto
            {
                Id = social.Id,
                Platform = social.Platform,
                Url = social.Url,
                Icon = social.Icon
            };
        }

        public async Task<SocialReadDto> CreateSocialAsync(SocialCreateDto socialCreateDto)
        {
            var social = new SocialModel
            {
                Id = Guid.NewGuid(),
                Platform = socialCreateDto.Platform,
                Url = socialCreateDto.Url,
                Icon = socialCreateDto.Icon
            };

            await _context.Socials.AddAsync(social);
            await _context.SaveChangesAsync();

            return new SocialReadDto
            {
                Id = social.Id,
                Platform = social.Platform,
                Url = social.Url,
                Icon = social.Icon
            };
        }

        public async Task<SocialReadDto> UpdateSocialAsync(Guid id, SocialUpdateDto socialUpdateDto)
        {
            var social = await _context.Socials.FindAsync(id);
            if (social == null) return null;

            social.Platform = socialUpdateDto.Platform;
            social.Url = socialUpdateDto.Url;
            social.Icon = socialUpdateDto.Icon;

            _context.Socials.Update(social);
            await _context.SaveChangesAsync();

            return new SocialReadDto
            {
                Id = social.Id,
                Platform = social.Platform,
                Url = social.Url,
                Icon = social.Icon
            };
        }

        public async Task<bool> DeleteSocialAsync(Guid id)
        {
            var social = await _context.Socials.FindAsync(id);
            if (social == null) return false;

            _context.Socials.Remove(social);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}