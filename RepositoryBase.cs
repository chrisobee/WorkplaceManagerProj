using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Data;

namespace WorkplaceManager
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        public RepositoryBase(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }
        public async Task<IQueryable<T>> FindAll()
        {
            return ApplicationDbContext.Set<T>();
        }

        public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return ApplicationDbContext.Set<T>().Where(expression);
        }

        public void Create(T entity)
        {
            ApplicationDbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            ApplicationDbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            ApplicationDbContext.Set<T>().Remove(entity);
        }
    }
}
