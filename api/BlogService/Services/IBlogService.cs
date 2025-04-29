using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogService.DTOs;

namespace BlogService.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogReadDto>> GetAllBlogsAsync();
        Task<BlogReadDto> GetBlogByIdAsync(Guid id);
        Task<BlogReadDto> CreateBlogAsync(BlogCreateDto blogCreateDto); // Should return BlogReadDto
        Task<BlogReadDto> UpdateBlogAsync(Guid id, BlogUpdateDto blogUpdateDto); // Should return BlogReadDto
        Task<bool> DeleteBlogAsync(Guid id);
        Task<IEnumerable<BlogReadDto>> GetBlogsByAuthorAsync(string author);
    }
}
