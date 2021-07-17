using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Net5WebApiUnitOfWork.Core
{
    public interface IGenericRepository<T> where T:class
    {
        Task<IEnumerable<T>> All();
        Task<T> GetById(Guid Id);
        Task<bool> Add(T entity);
        Task<bool> Delete(Guid Id);
        Task<bool> Update(T entity);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);

    }
}
