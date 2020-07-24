using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ford.Models;

namespace Ford.Core.Abstracts
{
    public interface IRepository<T> : IDisposable where T : EntityBase
    {
        T Find(int id);
        IQueryable<T> Query();
        IQueryable<T> Query(Expression<Func<T, bool>> predict);
        void Create(T entity);
        Task CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entity);
        void Update(T entity);
        void Delete(T entity);
        void Add(T entity);
        void Modify(T entity);
        void Remove(T entity);
        
    }
}