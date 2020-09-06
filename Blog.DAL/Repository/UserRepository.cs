using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Repository
{
    public class UserRepository : IRepository<User>
    {
        readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public Task Create(User user)
        {
            if (user != null)
                return Task.Run(async () =>
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                });
            return Task.FromResult(false);
        }

        public Task Delete(int id)
        {
            return Task.Run(async () =>
           {
               var user = await _context.Users.FindAsync(id);
               _context.Users.Remove(user);
               await _context.SaveChangesAsync();

           });
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _context.Users.Include(u => u.Blogs).ToListAsync();
            if (users != null)
                return users;
            return new List<User>();
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users.Include(u => u.Blogs)
                        .SingleOrDefaultAsync(u => u.UserId == id);
            if (user != null)
                return user;
            return new User();
        }

        public Task Update(User user)
        {
            return Task.Run(async () =>
                 {
                     _context.Entry(user).State = EntityState.Modified;
                     await _context.SaveChangesAsync();
                 });
        }
    }
}
