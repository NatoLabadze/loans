using Core.Application.Interfaces.Repository;
using Core.Application.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected LoanDbContext context;
        public Repository(LoanDbContext context)
        {
            this.context = context;
        }


        public async Task Add(T entity)
        {

            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task <bool> Existed(int id)
        {

            var result = await context.Set<T>().FindAsync(id);
            if (result != null)
                return true;
            else
                return false;
        }

     
        public async Task<IEnumerable<T>> GetAll(PageRequest pageRequest = null, Expression<Func<T, bool>> clause = null, params Expression<Func<T, object>>[] includes)
        {

            var query = context.Set<T>().AsQueryable();

            if (clause != null)
            {
                query = query.Where(clause);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));

            }
       
            if (pageRequest != null)
            {
                query = query.Skip((pageRequest.PageNumber - 1) * pageRequest.PageSize).Take(pageRequest.PageSize);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task< T> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);

        }
        public  async Task Update(int id, T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

    }
}
