using Blog.DAL.Repository;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DAL
{
    public static class Configure
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            //Context lifetime defaults to "scoped"
            services
                 .AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Blog.Models.Blog>, BlogRepository>();
        }
    }
}
