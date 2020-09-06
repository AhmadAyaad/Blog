using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.API.Dto;
using Blog.DAL.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        readonly IRepository<User> _userRepostiory;

        public UserController(IRepository<User> userRepostiory)
        {
            _userRepostiory = userRepostiory;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepostiory.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepostiory.GetAll();
            if (users == null)
                return NotFound();
            return Ok(users);

        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, UserForUpdateDto userForUpdateDto)
        {
            if (userForUpdateDto == null)
                return BadRequest("User can not be empty");

            if (userId != Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _userRepostiory.GetById(userId);

            user.Email = userForUpdateDto.Email;
            user.Name = userForUpdateDto.Name;
            user.Blogs = userForUpdateDto.Blogs;
            user.CreatedAt = userForUpdateDto.CreatedAt;
            user.Phone = userForUpdateDto.Phone;

            await _userRepostiory.Update(user);
            return Ok(userForUpdateDto);
            throw new Exception("cannot update");
        }

    }
}
