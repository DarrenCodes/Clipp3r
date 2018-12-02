using Clipp3r.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    class GenericPersistenceHandler<TEntity> : IGenericPersistenceHandler<TEntity> where TEntity : Entity
    {
        IManageRespository<TEntity> ManageRespository { get; }

        public GenericPersistenceHandler(IManageRespository<TEntity> manageRespository)
        {
            ManageRespository = manageRespository;
        }

        public virtual TEntity Add(TEntity entity)
        {
            return ManageRespository.Add(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            return ManageRespository.Update(entity);
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, 
            params Expression<Func<TEntity, object>>[] includes)
        {
            return ManageRespository.FirstOrDefaultAsync(
                ManageRespository.Include(ManageRespository.DataSet(predicate), includes));
        }

        private IQueryable<TProperty> GetSelectQuery<TProperty>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TProperty>> selector)
        {
            return ManageRespository.DataSet(predicate).Select(selector);
        }

        public Task<TProperty> GetSelectAsync<TProperty>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TProperty>> selector)
        {
            return ManageRespository.FirstOrDefaultAsync(GetSelectQuery(predicate, selector));
        }
        
        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, 
            params Expression<Func<TEntity, object>>[] includes)
        {
            return ManageRespository.ToListAsync(
                ManageRespository.Include(ManageRespository.DataSet(predicate), includes));
        }

        public Task<List<TProperty>> GetSelectListAsync<TProperty>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TProperty>> selector)
        {
            return ManageRespository.ToListAsync(GetSelectQuery(predicate, selector));
        }
    }
}
