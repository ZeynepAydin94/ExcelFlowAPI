using System;
using System.Linq.Expressions;
using AutoMapper;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;

namespace ExcelFlow.Services.Services;


public class BaseService<TEntity, TCreateDto, TUpdateDto, TResponse> : IBaseService<TEntity, TCreateDto, TUpdateDto, TResponse>
    where TEntity : class, new()
{
    protected readonly IBaseRepository<TEntity> _repository;
    protected readonly IMapper _mapper;

    public BaseService(
        IBaseRepository<TEntity> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<TEntity> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new KeyNotFoundException($"{id} not found");

        return _mapper.Map<TEntity>(entity);
    }
    public async Task<TResponse> CreateAsync(TCreateDto dto)
    {
        // await ValidateCreateAsync(dto);

        var entity = _mapper.Map<TEntity>(dto);
        await PreInsertAsync(entity);
        await _repository.AddAsync(entity);
        await PostInsertAsync(entity);

        return _mapper.Map<TResponse>(entity);
    }
    public async Task<TResponse> UpdateAsync(int id, TUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new Exception($"{typeof(TEntity).Name} with ID {id} not found.");

        await PreUpdateAsync(entity, dto);

        _mapper.Map(dto, entity); // dto'dan var olan entity'ye kopya

        await _repository.UpdateAsync(entity);
        await PostUpdateAsync(entity);

        return _mapper.Map<TResponse>(entity);
    }
    public virtual Task PreInsertAsync(TEntity entity) => Task.CompletedTask;
    public virtual Task PostInsertAsync(TEntity entity) => Task.CompletedTask;
    public virtual Task PreUpdateAsync(TEntity entity, TUpdateDto dto) => Task.CompletedTask;
    public virtual Task PostUpdateAsync(TEntity entity) => Task.CompletedTask;
}