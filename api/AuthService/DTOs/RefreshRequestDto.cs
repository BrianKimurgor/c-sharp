using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.DTOs
{
    public class RefreshRequestDto
    {
        public string RefreshToken { get; set; } = null!;
    }

}