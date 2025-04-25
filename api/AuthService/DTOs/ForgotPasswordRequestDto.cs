using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.DTOs
{
    public class ForgotPasswordRequestDto
    {
        public required string Email { get; set; } = null!;
    }
}