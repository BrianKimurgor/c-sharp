using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogService.DTOs;
using BlogService.Models;

namespace BlogService.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogReadDto>> GetAllBlogsAsync();
        Task<BlogReadDto> GetBlogByIdAsync(Guid id);
        Task<BlogCreateDto> CreateBlogAsync(BlogCreateDto blogCreateDto);
        Task<BlogUpdateDto> UpdateBlogAsync(Guid id, BlogUpdateDto blogUpdateDto);
        Task<bool> DeleteBlogAsync(Guid id);
        Task<IEnumerable<BlogReadDto>> GetBlogsByAuthorAsync(string author);
    }
}