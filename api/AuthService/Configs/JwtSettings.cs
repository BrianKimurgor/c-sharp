using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Configs
{
    public class JwtSettings
    {
        public required string Issuer { get; set; }  = "";      // e.g. "YourApp"
        public required string Audience { get; set; }  = "";             // e.g. "YourAppUsers"
        public required string SecretKey { get; set; }   = "";           // your symmetric key
        public int AccessTokenExpirationMinutes { get; set; }  // e.g. 60
        public int RefreshTokenExpirationDays { get; set; }    // e.g. 30
    }
}