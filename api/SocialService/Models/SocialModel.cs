using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialService.Models
{
    public class SocialModel
    {
        public Guid Id { get; set; }
        public string Platform { get; set; } = string.Empty; // e.g., GitHub, LinkedIn
        public string Url { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty; // Optional: for frontend icon reference
    }
}