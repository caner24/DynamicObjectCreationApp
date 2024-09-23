using DynamicObjectCreationApp.Domain.Data.EntityFramework.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Domain.Data.EntityFramework.Concrete
{
    public class EFRepositoryBase<TContext, TEntity> : IEFRepositoryBase<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        private TContext _context;
        public EFRepositoryBase(TContext context)
        {
            _context = context;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter, bool isTracking)
        {
            return isTracking == false ? _context.Set<TEntity>().Where(filter).AsNoTracking() : _context.Set<TEntity>().Where(filter);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? _context.Set<TEntity>().AsNoTracking() : _context.Set<TEntity>().Where(filter).AsNoTracking();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
