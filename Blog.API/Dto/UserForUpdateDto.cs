using Blog.Models;
using System;
using System.Collections.Generic;

namespace Blog.API.Dto
{
    public class UserForUpdateDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public ICollection<Blog.Models.Blog> Blogs { get; set; } = new HashSet<Blog.Models.Blog>();
    }
}