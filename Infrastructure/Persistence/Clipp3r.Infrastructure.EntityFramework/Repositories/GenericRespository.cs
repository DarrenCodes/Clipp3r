using Clipp3r.Core.DomainLogic;
using Clipp3r.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clipp3r.Infrastructure.EntityFramework
{
    class GenericRespository<TEntity> : IManageRespository<TEntity> where TEntity : Entity
    {
        private readonly Clipp3rDatabaseContext clipp3rDatabaseContext;

        public GenericRespository(Clipp3rDatabaseContext clipp3rDatabaseContext)
        {
            this.clipp3rDatabaseContext = clipp3rDatabaseContext;
        }

        public TEntity Add(TEntity entity)
        {
            clipp3rDatabaseContext.Set<TEntity>().Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            return entity;
        }

        public IQueryable<T> DataSet<T>(Expression<Func<T, bool>> predicate = null) where T : Entity
        {
            var query = clipp3rDatabaseContext.Set<T>().AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            return query;
        }

        public IQueryable<T> Include<T>(IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : Entity
        {
            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            return query;
        }

        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query)
        {
            return query.FirstOrDefaultAsync();
        }

        public Task<List<T>> ToListAsync<T>(IQueryable<T> query)
        {
            return query.ToListAsync();
        }
    }
}
