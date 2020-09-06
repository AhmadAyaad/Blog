using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Repository
{
    public class BlogRepository : IRepository<Blog.Models.Blog>
    {
        readonly DataContext _context;

        public BlogRepository(DataContext context)
        {
            _context = context;
        }
        public async Task Create(Models.Blog blog)
        {
            if (blog != null)
            {
                await _context.Blogs.AddAsync(blog);
                await _context.SaveChangesAsync();
            }
        }

        public Task Delete(int id)
        {
            return Task.Run(async () =>
            {
                var blog = await _context.Blogs.FindAsync(id);
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
            });
        }

        public async Task<List<Models.Blog>> GetAll()
        {
            var blogs = await _context.Blogs.ToListAsync();
            if (blogs != null)
                return blogs;
            return new List<Models.Blog>();
        }

        public async Task<Models.Blog> GetById(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
                return blog;
            return new Models.Blog();
        }

        public Task Update(Models.Blog blog)
        {
            return Task.Run(async () =>
            {
                _context.Entry(blog).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            });
        }
    }
}
