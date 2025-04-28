using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadgeService.Models
{
    public class Badge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}