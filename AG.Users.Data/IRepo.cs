using AG.Users.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AG.Users.Data
{
    public interface IRepo<T> where T : class, IUser
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<List<T>> GetByName(string name);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    }
}
