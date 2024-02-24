using Core.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

namespace Core.DataAccess.EntityFramework
{
    public interface IGenericRepository<T> where T : class, IEntity,new()
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            params Expression<Func<T, object>>[] includeProperties);

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task<List<T>> SearchAsync(List<Expression<Func<T, bool>>> predicates,
            params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(List<Expression<Func<T, bool>>> predicates=null, List<Expression<Func<T, object>>> includeProperties = null);
        Task<List<T>> GetAllAsync(List<Expression<Func<T, bool>>> predicates, List<Expression<Func<T, object>>> includeProperties);
    }
}
