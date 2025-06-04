using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.DTOs
{
    public class ProfileReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Company { get; set; } = null!;
        public string Bio { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}