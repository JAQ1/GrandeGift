using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GrandeGift.Services
{
    public interface IRepository<T>
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetSingle(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Query(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
    }
}
