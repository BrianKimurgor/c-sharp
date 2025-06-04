using ImageUploadService.DTOs;
using ImageUploadService.Models;
using ImageUploadService.Repositories;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ImageUploadService.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IWebHostEnvironment _env;

        public ImageService(IImageRepository imageRepository, IWebHostEnvironment env)
        {
            _imageRepository = imageRepository;
            _env = env;
        }

        public async Task<ImageResponseDto> UploadImageAsync(ImageUploadDto imageUploadDto)
        {
            var image = imageUploadDto.Image;

            if (image == null || image.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            // Validate file extension
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var ext = Path.GetExtension(image.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || Array.IndexOf(allowedExtensions, ext) < 0)
            {
                throw new ArgumentException("Invalid image file type.");
            }

            var uploadFolder = Path.Combine(_env.ContentRootPath, "UploadedImages");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var url = Path.Combine("/UploadedImages", uniqueFileName).Replace("\\", "/");


            // Save metadata via repository (simulate)
            var imageModel = new Image
            {
                FileName = uniqueFileName,
                Url = url,
                UploadedAt = DateTime.UtcNow
            };

            await _imageRepository.UploadImageAsync(imageModel);

            return new ImageResponseDto
            {
                Id = imageModel.Id,
                FileName = imageModel.FileName,
                Url = imageModel.Url,
                UploadedAt = imageModel.UploadedAt
            };
        }
        public async Task<ImageResponseDto?> GetImageByIdAsync(int id)
        {
            try
            {
                var image = await _imageRepository.GetImageByIdAsync(id);
                if (image == null)
                {
                    return null;
                }

                return new ImageResponseDto
                {
                    Id = image.Id,
                    FileName = image.FileName,
                    Url = image.Url,
                    UploadedAt = image.UploadedAt
                };
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new ApplicationException("An error occurred while retrieving the image.", ex);
            }
        }

        public async Task<IEnumerable<ImageResponseDto>> GetAllImagesAsync()
        {
            var images = await _imageRepository.GetAllImagesAsync();
            var imageDtos = new List<ImageResponseDto>();

            foreach (var image in images)
            {
                imageDtos.Add(new ImageResponseDto
                {
                    Url = image.Url
                });
            }

            return imageDtos;
        }

        public async Task<bool> DeleteImageAsync(int id)
        {
            var image = await _imageRepository.GetImageByIdAsync(id);
            if (image == null)
            {
                return false;
            }

            // Delete the file from the server
            var filePath = Path.Combine(_env.ContentRootPath, "UploadedImages", image.FileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Remove metadata via repository
            return await _imageRepository.DeleteImageAsync(id);
        }
    }
}
