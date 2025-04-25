using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Models;
using AuthService.Data;

namespace AuthService.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        // Assuming you have a DbContext injected here for database operations
        private readonly AuthDbContext _context;

        public RefreshTokenRepository(AuthDbContext context)
        {
            _context = context;
        }

        public void Add(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();
        }

        public void Update(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            _context.SaveChanges();
        }
        public void Revoke(string token, string revokedByIp, string replacedByToken)
        {
            var refreshToken = _context.RefreshTokens.SingleOrDefault(rt => rt.Token == token);
            if (refreshToken != null)
            {
                refreshToken.Revoked = DateTime.UtcNow;
                refreshToken.RevokedByIp = revokedByIp;
                refreshToken.ReplacedByToken = replacedByToken;
                _context.SaveChanges();
            }
        }

        public RefreshToken? GetByToken(string token)
        {
            return _context.RefreshTokens.SingleOrDefault(rt => rt.Token == token);
        }

        
    }
}