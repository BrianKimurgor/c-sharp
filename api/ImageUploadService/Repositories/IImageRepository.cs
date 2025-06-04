using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageUploadService.Models;

namespace ImageUploadService.Repositories
{
    public interface IImageRepository
    {
        Task<Image> UploadImageAsync(Image image);
        Task<Image?> GetImageByIdAsync(int id);
        Task<IEnumerable<Image>> GetAllImagesAsync();
        Task<bool> DeleteImageAsync(int id);
    }
}