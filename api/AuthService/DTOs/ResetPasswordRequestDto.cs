using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.DTOs
{
    public class ResetPasswordRequestDto
    {
        public required string NewPassword { get; set; } = null!;
        public required string ResetToken { get; set; }

    }

}