using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.DTOs
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public required string Username { get; set; }
        public required string Email { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

}