using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using GrandeGift.Data;

namespace GrandeGift.Services
{
    public class BaseRepository<T> : IRepository<T> where T: class
    {
        private ApplicationDbContext dbContext;
        private DbSet<T> dbTable;

        public BaseRepository()
        {
            dbContext = new ApplicationDbContext();
            dbTable = dbContext.Set<T>();
        }

        public void Create(T entity)
        {
            dbTable.Add(entity);
            dbContext.SaveChanges();
        }
        public void Update(T entity)
        {
            dbTable.Update(entity);
            dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            dbTable.Remove(entity);
            dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return dbTable.ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Where(predicate);
        }

        
    }
}
