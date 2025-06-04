using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageUploadService.DTOs;

namespace ImageUploadService.Services
{
    public interface IImageService
    {
        Task<ImageResponseDto> UploadImageAsync(ImageUploadDto imageUploadDto);
        Task<ImageResponseDto?> GetImageByIdAsync(int id);
        Task<IEnumerable<ImageResponseDto>> GetAllImagesAsync();
        Task<bool> DeleteImageAsync(int id);
    }
}