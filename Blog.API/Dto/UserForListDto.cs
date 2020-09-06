using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Dto
{
    public class UserForListDto
    {
        public string Name { get; set; }
        public int? Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Blog.Models.Blog> Blogs { get; set; } = new HashSet<Blog.Models.Blog>();
    }
}
