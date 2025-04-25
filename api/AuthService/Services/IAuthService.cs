using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthService.Models;
using AuthService.DTOs;

namespace AuthService.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto,string createdByIp);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto dto, string createdByIp);
        Task<AuthResponseDto> RefreshAsync(string refreshToken);
    }
}