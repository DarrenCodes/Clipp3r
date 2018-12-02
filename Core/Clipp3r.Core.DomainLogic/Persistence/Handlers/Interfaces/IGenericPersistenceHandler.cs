using Clipp3r.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    public interface IGenericPersistenceHandler<TEntity> where TEntity : Entity
    {
        TEntity Add(TEntity entity);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);
        Task<TProperty> GetSelectAsync<TProperty>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TProperty>> selector);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);
        Task<List<TProperty>> GetSelectListAsync<TProperty>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TProperty>> selector);
        TEntity Update(TEntity entity);
    }
}