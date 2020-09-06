using System;

namespace Blog.API.Dto
{
    public class UserForCreateDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Phone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Name { get; set; }
    }
}