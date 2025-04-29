using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogService.DTOs;
using BlogService.Models;
using BlogService.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogService.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly List<BlogReadDto> _blogs = new List<BlogReadDto>();

        public Task<IEnumerable<BlogReadDto>> GetAllBlogsAsync()
        {
            return Task.FromResult(_blogs.AsEnumerable());
        }

        public Task<BlogReadDto?> GetBlogByIdAsync(Guid id)
        {
            var blog = _blogs.FirstOrDefault(b => b.Id == id);
            return Task.FromResult(blog); // It can be null, which is expected
        }

        public Task<BlogCreateDto> CreateBlogAsync(BlogCreateDto blogCreateDto)
        {
            var newBlog = new BlogReadDto
            {
                Id = Guid.NewGuid(),
                Title = blogCreateDto.Title,
                Content = blogCreateDto.Content,
                Author = blogCreateDto.Author,
                ImageUrl = blogCreateDto.ImageUrl,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _blogs.Add(newBlog);

            return Task.FromResult(blogCreateDto); // Return the BlogCreateDto
        }

        public Task<BlogUpdateDto?> UpdateBlogAsync(Guid id, BlogUpdateDto blogUpdateDto)
        {
            var blog = _blogs.FirstOrDefault(b => b.Id == id);
            if (blog != null)
            {
                blog.Title = blogUpdateDto.Title;
                blog.Content = blogUpdateDto.Content;
                blog.Author = blogUpdateDto.Author;
                blog.ImageUrl = blogUpdateDto.ImageUrl;
                blog.UpdatedAt = DateTime.UtcNow;
            }
            return Task.FromResult(blogUpdateDto); // Return the BlogUpdateDto
        }

        public Task<bool> DeleteBlogAsync(Guid id)
        {
            var blog = _blogs.FirstOrDefault(b => b.Id == id);
            if (blog != null)
            {
                _blogs.Remove(blog);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<IEnumerable<BlogReadDto>> GetBlogsByAuthorAsync(string author)
        {
            var blogsByAuthor = _blogs.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(blogsByAuthor.AsEnumerable());
        }
    }
}
