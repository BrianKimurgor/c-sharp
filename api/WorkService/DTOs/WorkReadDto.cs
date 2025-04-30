using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkService.DTOs
{
    public class WorkReadDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}