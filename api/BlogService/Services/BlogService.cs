using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogService.DTOs;
using BlogService.Models;
using BlogService.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogService.Services
{
    public class BlogService : IBlogService
    {
        private readonly BlogDbContext _context;

        public BlogService(BlogDbContext context)
        {
            _context = context;
        }

        // Get all blogs
        public async Task<IEnumerable<BlogReadDto>> GetAllBlogsAsync()
        {
            var blogs = await _context.Blogs.ToListAsync();
            return blogs.Select(b => new BlogReadDto
            {
                Id = b.Id,
                Title = b.Title,
                Content = b.Content,
                Author = b.Author,
                ImageUrl = b.ImageUrl
            });
        }

        // Get a single blog by its ID
        public async Task<BlogReadDto> GetBlogByIdAsync(Guid id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return null;

            return new BlogReadDto
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Author = blog.Author,
                ImageUrl = blog.ImageUrl
            };
        }

        // Create a new blog
        public async Task<BlogReadDto> CreateBlogAsync(BlogCreateDto blogCreateDto)
        {
            var blog = new BlogModel
            {
                Id = Guid.NewGuid(),
                Title = blogCreateDto.Title,
                Content = blogCreateDto.Content,
                Author = blogCreateDto.Author,
                ImageUrl = blogCreateDto.ImageUrl,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return new BlogReadDto
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Author = blog.Author,
                ImageUrl = blog.ImageUrl
            };
        }

        // Update an existing blog
        public async Task<BlogReadDto> UpdateBlogAsync(Guid id, BlogUpdateDto blogUpdateDto)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return null;

            blog.Title = blogUpdateDto.Title;
            blog.Content = blogUpdateDto.Content;
            blog.Author = blogUpdateDto.Author;
            blog.ImageUrl = blogUpdateDto.ImageUrl;
            blog.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new BlogReadDto
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Author = blog.Author,
                ImageUrl = blog.ImageUrl
            };
        }

        // Delete a blog by its ID
        public async Task<bool> DeleteBlogAsync(Guid id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return false;

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return true;
        }

        // Get all blogs by a specific author
        public async Task<IEnumerable<BlogReadDto>> GetBlogsByAuthorAsync(string author)
        {
            var blogs = await _context.Blogs.Where(b => b.Author == author).ToListAsync();
            return blogs.Select(b => new BlogReadDto
            {
                Id = b.Id,
                Title = b.Title,
                Content = b.Content,
                Author = b.Author,
                ImageUrl = b.ImageUrl
            });
        }
    }
}
