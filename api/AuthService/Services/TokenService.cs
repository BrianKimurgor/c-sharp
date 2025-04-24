using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace AuthService.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;
        private readonly IUserRepository _userRepository; // Ensure this is injected

        public TokenService(IOptions<JwtSettings> opts, IUserRepository userRepository)
        {
            _settings = opts.Value;
            _userRepository = userRepository;
        }

        public AuthResponseDto GenerateTokens(User user)
        {
            // **1. Generate a guaranteed 256-bit (32-byte) key**
            byte[] key;
            if (_settings.SecretKey.Length < 32)
            {
                // If key is too short, hash it to 32 bytes (SHA256 produces 256-bit output)
                key = SHA256.HashData(Encoding.UTF8.GetBytes(_settings.SecretKey));
            }
            else
            {
                // If key is long enough, use it directly (truncate/pad to 32 bytes if needed)
                key = Encoding.UTF8.GetBytes(_settings.SecretKey.PadRight(32).Substring(0, 32));
            }

            // **2. Create JWT access token**
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpirationMinutes);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: new[] { new Claim(ClaimTypes.Name, user.Username) },
                expires: expires,
                signingCredentials: creds
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            // **3. Generate a secure refresh token (64 random bytes)**
            var refreshTokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(refreshTokenBytes);
            
            // **4. Save hashed refresh token to DB (optional but recommended)**
            var hashedRefreshToken = Convert.ToBase64String(SHA256.HashData(refreshTokenBytes));
            user.RefreshToken = hashedRefreshToken;
            _userRepository.UpdateUser(user);

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken, // Return raw token to the client
                ExpiresAt = expires
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