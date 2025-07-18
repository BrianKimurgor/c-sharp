using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationService.DTOs
{
    public class EducationUpdateDto
    {
        public string SchoolName { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public string FieldOfStudy { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}