using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.API.Dto;
using Blog.DAL;
using Blog.DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/user/{userId}/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BlogController : ControllerBase
    {
        readonly IRepository<Blog.Models.Blog> _blogRepository;

        public BlogController(IRepository<Blog.Models.Blog> blogRepository)
        {
            _blogRepository = blogRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            var blogs = await _blogRepository.GetAll();
            var LoggedInUserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            blogs = blogs.Where(b => b.UserId == LoggedInUserId).ToList();
            if (blogs != null)
                return Ok(blogs);
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog(int id)
        {
            var blog = await _blogRepository.GetById(id);
            if (blog != null)
                return Ok(blog);
            return NotFound();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBlog(BlogForCreateDto blogForCreateDto)
        {
            if (blogForCreateDto == null)
                return BadRequest("Blog data can not be empty");

            blogForCreateDto.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var blog = new Blog.Models.Blog
            {
                Content = blogForCreateDto.Content,
                CreatedAt = blogForCreateDto.CreatedAt,
                Title = blogForCreateDto.Title,
                UserId = blogForCreateDto.UserId
            };
            try
            {
                await _blogRepository.Create(blog);
                return StatusCode(201);
            }

            catch (Exception)
            {
                throw;
            }

        }
        [Authorize]
        [HttpDelete("{blogId}")]
        public async Task<IActionResult> DeleteBlog(int userId, int blogId)
        {
            if (userId != Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            try
            {
                await _blogRepository.Delete(blogId);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

