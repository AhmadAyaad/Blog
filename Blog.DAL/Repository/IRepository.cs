using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        Task Create(T t);
        Task Update(T t );
        Task Delete(int id);
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
    }
}
