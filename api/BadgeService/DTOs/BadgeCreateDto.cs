using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadgeService.DTOs
{
    public class BadgeCreateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}