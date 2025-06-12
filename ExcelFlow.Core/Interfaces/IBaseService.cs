using System;

namespace ExcelFlow.Core.Interfaces;

public interface IBaseService<TEntity, TCreateDto, TRepsonse> where TEntity : class
{
    // Task<TEntity?> GetByIdAsync(int id);
    // Task<IEnumerable<TEntity>> GetAllAsync();
    // Task<IEnumerable<TEntity>> FindAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
    // Task AddAsync(TEntity entity);
    // Task UpdateAsync(TEntity entity);
    // Task DeleteAsync(TEntity entity);
    Task<TRepsonse> CreateAsync(TCreateDto dto);
    Task PreInsertAsync(TEntity entity);
    Task PostInsertAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(int id);
}