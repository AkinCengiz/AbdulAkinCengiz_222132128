using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess;
public interface IGenericRepository<T> where T : class, IEntity, new()
{
    Task<T?> GetAsync(Expression<Func<T, bool>> filter);
    IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null);
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}
