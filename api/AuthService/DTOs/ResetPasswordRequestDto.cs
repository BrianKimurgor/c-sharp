using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.DTOs
{
    public class ResetPasswordRequestDto
    {
        public string Email { get; set; } = null!;
        // you might include a reset token, new password, etc.
    }

}