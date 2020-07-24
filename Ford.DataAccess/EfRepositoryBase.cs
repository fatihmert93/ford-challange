using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ford.Core.Abstracts;
using Ford.Models;
using Microsoft.EntityFrameworkCore;

namespace Ford.DataAccess
{
   public abstract class EfRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected EfRepositoryBase(FordDbContext entityContext)
        {
            _context = entityContext;
            _type = typeof(TEntity);
        }

        private Type _type;
        public bool AutoTableGenerate { get; set; } = false;


        private DbContext _context;


        protected DbContext Context => _context;
        

        public virtual void Create(TEntity entity)
        {
            Add(entity);
            Context.SaveChanges();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
        }
        
        public async Task CreateRangeAsync(IEnumerable<TEntity> entity)
        {
            await Context.AddRangeAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }
        
        public async Task DeleteRangeAsync(IEnumerable<TEntity> entity)
        {
            Context.RemoveRange(entity);
            await Context.SaveChangesAsync();
        }

        public virtual void Update(TEntity entity)
        {
            Modify(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            Remove(entity);
            Context.SaveChanges();
        }

        public void Add(TEntity entity)
        {
            Context.Add(entity);
        }

        public void Modify(TEntity entity)
        {
            
            Context.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            Context.Update(entity);
        }

        public void Commit()
        {
            Context.SaveChanges();
        }
        
        public virtual TEntity Find(int id)
        {
            return Context.Set<TEntity>().SingleOrDefault(v => v.Id == id);
        }
        

        public virtual IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predict)
        {
            return Context.Set<TEntity>().Where(predict).AsNoTracking();
        }
        

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

        
    }
}