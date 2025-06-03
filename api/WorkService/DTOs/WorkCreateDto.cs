using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkService.DTOs
{
    public class WorkCreateDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Responsibilities { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public string Location { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}