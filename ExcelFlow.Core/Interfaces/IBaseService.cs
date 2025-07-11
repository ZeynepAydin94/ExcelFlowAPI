using System;

namespace ExcelFlow.Core.Interfaces;

public interface IBaseService<TEntity, TCreateDto, TUpdateDto, TRepsonse> where TEntity : class
{

    Task<TRepsonse> CreateAsync(TCreateDto dto);
    Task PreInsertAsync(TEntity entity);
    Task PostInsertAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(int id);
    Task<TRepsonse> UpdateAsync(int id, TUpdateDto dto);
    Task PreUpdateAsync(TEntity entity, TUpdateDto dto);
    Task PostUpdateAsync(TEntity entity);
}