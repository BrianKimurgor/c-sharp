using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogService.DTOs;
using BlogService.Models;
using BlogService.Repositories;
using BlogService.Services;
using Microsoft.AspNetCore.Mvc;


namespace BlogService.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await _blogService.GetAllBlogsAsync();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(Guid id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null) return NotFound();
            return Ok(blog);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] BlogCreateDto blogCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdBlog = await _blogService.CreateBlogAsync(blogCreateDto);
            return CreatedAtAction(nameof(GetBlogById), new { id = createdBlog.Id }, createdBlog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(Guid id, [FromBody] BlogUpdateDto blogUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedBlog = await _blogService.UpdateBlogAsync(id, blogUpdateDto);
            if (updatedBlog == null) return NotFound();
            return Ok(updatedBlog);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            var result = await _blogService.DeleteBlogAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("author/{author}")]
        public async Task<IActionResult> GetBlogsByAuthor(string author)
        {
            var blogs = await _blogService.GetBlogsByAuthorAsync(author);
            return Ok(blogs);
        }
    }
};