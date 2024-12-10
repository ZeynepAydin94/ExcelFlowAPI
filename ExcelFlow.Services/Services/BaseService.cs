using System;
using System.Linq.Expressions;
using ExcelFlow.Core.Interfaces;

namespace ExcelFlow.Services.Services;
public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
{
    private readonly IRepository<TEntity> _repository;

    public BaseService(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _repository.FindAsync(predicate);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        await _repository.DeleteAsync(entity);
    }
}