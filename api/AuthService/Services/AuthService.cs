using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthService.Models;
using AuthService.DTOs;
using AuthService.Repositories;
using Microsoft.IdentityModel.Tokens;           // <— SigningCredentials, SymmetricSecurityKey, SecurityAlgorithms
using System.IdentityModel.Tokens.Jwt;  

namespace AuthService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly ITokenService _tokenSvc;
        public AuthService(IUserRepository repo, ITokenService tokenSvc)
        {
            _repo = repo;
            _tokenSvc = tokenSvc;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            var exists = await _repo.GetUserByUsernameAsync(dto.Username);
            if (exists != null) throw new Exception("User already exists");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            await _repo.CreateUserAsync(user);
            await _repo.SaveChangesAsync();

            return _tokenSvc.GenerateTokens(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _repo.GetUserByUsernameAsync(dto.Username)
                        ?? throw new Exception("Invalid credentials");
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            return _tokenSvc.GenerateTokens(user);
        }

        public async Task<AuthResponseDto> RefreshAsync(string refreshToken)
        {
            // lookup token in DB, validate expiry, rotate, etc.
            // For now, throw if invalid:
            throw new NotImplementedException();
        }
    }

}