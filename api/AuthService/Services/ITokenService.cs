using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthService.Models;
using AuthService.DTOs;
using AuthService.Repositories;
using System.Security.Claims; 

namespace AuthService.Services
{
    public interface ITokenService
{
    AuthResponseDto GenerateTokens(User user);
    ClaimsPrincipal? ValidateToken(string token);
}

}