using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadgeService.DTOs
{
    public class BadgeResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;  
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}