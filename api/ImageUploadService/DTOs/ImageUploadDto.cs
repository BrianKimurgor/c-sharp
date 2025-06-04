using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImageUploadService.DTOs
{
    public class ImageUploadDto
    {
        public IFormFile Image { get; set; }
    }
}