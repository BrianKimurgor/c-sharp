using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageUploadService.Models;
using ImageUploadService.Data;
using Microsoft.EntityFrameworkCore;

namespace ImageUploadService.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ImageUploadDbContext _dbContext;

        public ImageRepository(ImageUploadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Image>> GetAllImagesAsync()
        {
            return await _dbContext.Images.ToListAsync();
        }

        public async Task<Image?> GetImageByIdAsync(int id)
        {
            return await _dbContext.Images.FindAsync(id);
        }

        public async Task<Image> UploadImageAsync(Image image)
        {
            _dbContext.Images.Add(image);
            await _dbContext.SaveChangesAsync();
            return image;
        }

        public async Task<bool> DeleteImageAsync(int id)
        {
            var image = await _dbContext.Images.FindAsync(id);
            if (image == null) return false;

            _dbContext.Images.Remove(image);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
