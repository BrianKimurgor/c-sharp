using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Models;

namespace AuthService.Repositories
{
    public interface IRefreshTokenRepository
    {
        void Add(RefreshToken refreshToken); // Add a new refresh token to the database
        void Update(RefreshToken refreshToken); // Update an existing refresh token
        void Revoke(string token, string revokedByIp, string replacedByToken );
        RefreshToken? GetByToken(string token);
    }
}