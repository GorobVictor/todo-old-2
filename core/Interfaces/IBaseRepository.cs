using System.Linq.Expressions;
using core.Entities.Base;

namespace core.Interfaces;

public interface IBaseRepository<TEntity>
    where TEntity: FullEntity
{
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
    IQueryable<T> Get<T>(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> Get();
    IQueryable<T> Get<T>();
    Task<TEntity?> GetByIdAsync(int id, bool track = false);
    TEntity Add(TEntity entity);
    List<T> Add<T>(List<T> entities);
    Task<T> AddAsync<T>(T entity);
    Task<List<T>> AddAsync<T>(List<T> entities);
    Task<T> UpdateAsync<T>(T entity);
    Task<List<T>> UpdateAsync<T>(List<T> entities);
    T Update<T>(T entity);
    List<T> Update<T>(List<T> entities);
}