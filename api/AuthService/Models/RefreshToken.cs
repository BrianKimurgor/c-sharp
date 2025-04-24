using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }                  // Primary key
        public required string Token { get; set; }            // The refresh token string
        public DateTime Expires { get; set; }        // Expiration timestamp
        public DateTime Created { get; set; }        // When it was issued
        public required string CreatedByIp { get; set; }      // IP address that requested it

        public DateTime? Revoked { get; set; }       // When it was revoked (if at all)
        public required string RevokedByIp { get; set; }      // IP that revoked it
        public required string ReplacedByToken { get; set; }  // If rotated, the new token

        // Foreign key back to the User
        public required int UserId { get; set; }
        public required User User { get; set; }               // Navigation property
    }
}