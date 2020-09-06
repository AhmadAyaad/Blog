using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int? Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Role { get; set; } = "Admin";
        public ICollection<Blog> Blogs { get; set; } = new HashSet<Blog>();


    }
}
