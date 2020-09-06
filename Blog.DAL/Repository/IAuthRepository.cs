using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Repository
{
    public interface IAuthRepository
    {
        Task<User> Login(string email, string password);
        Task<User> Register(User user, string password);
        Task<bool> UserExists(string email);
    }
}
