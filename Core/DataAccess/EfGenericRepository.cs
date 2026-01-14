using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess;
public abstract class EfGenericRepository<TEntity,TContext> : IGenericRepository<TEntity>
where TEntity : class, IEntity, new()
where TContext : DbContext
{
    private readonly TContext _context;
    private readonly DbSet<TEntity> _dbSet;

    protected EfGenericRepository(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _dbSet.FirstOrDefaultAsync(filter);
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
    {
        return filter == null ? _dbSet : _dbSet.Where(filter);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _dbSet.AnyAsync(filter);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        entity.UpdateAt = DateTime.Now;
        _dbSet.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void SoftDelete(TEntity entity)
    {
        entity.UpdateAt = DateTime.Now;
        entity.IsDeleted = true;
        entity.IsActive = false;
        _dbSet.Update(entity);
    }
}
