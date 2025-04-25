using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using AuthService.Models;
using AuthService.Repositories;
using AuthService.DTOs;
using AuthService.Configs;

namespace AuthService.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;
        private readonly IUserRepository _userRepository; // Ensure this is injected
        private readonly IRefreshTokenRepository _refreshTokenRepository; // Ensure this is injected

        public TokenService(IOptions<JwtSettings> opts, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _settings = opts.Value;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository; // Assuming you have a way to get this repository
        }

        public AuthResponseDto GenerateTokens(User user, string createdByIp)
        {
            // 1. Generate key
            byte[] key = _settings.SecretKey.Length < 32
                ? SHA256.HashData(Encoding.UTF8.GetBytes(_settings.SecretKey))
                : Encoding.UTF8.GetBytes(_settings.SecretKey.PadRight(32).Substring(0, 32));

            // 2. Create claims and token
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpirationMinutes);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Email, user.Email)
    };

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            // 3. Refresh Token
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(_settings.RefreshTokenExpirationDays),
                CreatedByIp = createdByIp,
                RevokedByIp = null!,
                ReplacedByToken = null!,
                UserId = user.Id,
                User = user
            };

            _refreshTokenRepository.Add(refreshTokenEntity); // Or await AddAsync if it's async

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = expires,
                Username = user.Username,
                Email = user.Email
                };
        }



        public ClaimsPrincipal? ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            // **Recompute the key the same way as in GenerateTokens**
            byte[] key;
            if (_settings.SecretKey.Length < 32)
            {
                key = SHA256.HashData(Encoding.UTF8.GetBytes(_settings.SecretKey));
            }
            else
            {
                key = Encoding.UTF8.GetBytes(_settings.SecretKey.PadRight(32).Substring(0, 32));
            }

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = _settings.Issuer,
                ValidAudience = _settings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                return handler.ValidateToken(token, parameters, out _);
            }
            catch
            {
                return null;
            }
        }
    }
}