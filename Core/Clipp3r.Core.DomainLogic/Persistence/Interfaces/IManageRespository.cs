using Clipp3r.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    public interface IManageRespository<TEntity> where TEntity : Entity
    {
        TEntity Add(TEntity entity);
        IQueryable<T> DataSet<T>(Expression<Func<T, bool>> predicate = null) where T : Entity;
        IQueryable<T> Include<T>(IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : Entity;
        Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query);
        Task<List<T>> ToListAsync<T>(IQueryable<T> query);
        TEntity Update(TEntity entity);
    }
}