using Core.Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repository
{
    public interface  IRepository<T> where T : class
    {
        Task Add(T entity);
        Task Update(int id, T entity);
        Task Delete(int id);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll(
           PageRequest pageRequest = null,
            Expression<Func<T, bool>> clause = null,
          params Expression<Func<T, object>>[] includes
         );
        Task<bool> Existed(int id);
    }
}
