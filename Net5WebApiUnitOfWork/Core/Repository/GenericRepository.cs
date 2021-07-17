using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Net5WebApiUnitOfWork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Net5WebApiUnitOfWork.Core.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext context;
        internal DbSet<T> dbSet;
        public readonly ILogger _logger;

        public GenericRepository(ApplicationDbContext context,  ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            _logger = logger;
        }

        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual  Task<IEnumerable<T>> All()
        {
            throw new NotImplementedException();
        }

        public virtual  Task<bool> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public virtual  Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {

            throw new NotImplementedException();
        }

        public virtual async Task<T> GetById(Guid Id)
        {
            return await dbSet.FindAsync(Id);
        }

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
