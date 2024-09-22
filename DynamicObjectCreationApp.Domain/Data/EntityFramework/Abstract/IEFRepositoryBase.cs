using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Domain.Data.EntityFramework.Abstract
{
    public interface IEFRepositoryBase<TEntity> where TEntity : class, IEntity, new()
    {
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter, bool isTracking = false);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task BulkUpdate(List<TEntity> entity);
        Task BulkDelete(Expression<Func<TEntity, bool>> filter);

    }
}
