using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialService.DTOs;
using SocialService.Models;
using SocialService.Data;
using SocialService.Repositories;
using Microsoft.EntityFrameworkCore;


namespace SocialService.Repositories
{
    public class SocialRepository : ISocialRepository
    {
        private readonly SocialDbContext _context;

        public SocialRepository(SocialDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SocialModel>> GetAllSocialsAsync()
        {
            return await _context.Socials.ToListAsync();
        }

        public async Task<SocialModel> GetSocialByIdAsync(Guid id)
        {
            return await _context.Socials.FindAsync(id);
        }

        public async Task<SocialModel> CreateSocialAsync(SocialModel social)
        {
            await _context.Socials.AddAsync(social);
            await _context.SaveChangesAsync();
            return social;
        }

        public async Task<bool> UpdateSocialAsync(Guid id, SocialModel social)
        {
            var existingSocial = await GetSocialByIdAsync(id);
            if (existingSocial == null) return false;

            existingSocial.Platform = social.Platform;
            existingSocial.Url = social.Url;
            existingSocial.Icon = social.Icon;

            _context.Socials.Update(existingSocial);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSocialAsync(Guid id)
        {
            var social = await GetSocialByIdAsync(id);
            if (social == null) return false;

            _context.Socials.Remove(social);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}