using System;
using System.Linq.Expressions;
using AutoMapper;
using ExcelFlow.Core.Interfaces;

namespace ExcelFlow.Services.Services;


public class BaseService<TEntity, TCreateDto> : IBaseService<TEntity, TCreateDto>
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
    public virtual async Task<TEntity> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new KeyNotFoundException($"{id} not found");

        return _mapper.Map<TEntity>(entity);
    }
    public virtual async Task<TCreateDto> CreateAsync(TCreateDto dto)
    {
        // await ValidateCreateAsync(dto);

        var entity = _mapper.Map<TEntity>(dto);

        await PreInsertAsync(entity);
        await _repository.AddAsync(entity);
        await PostInsertAsync(entity);

        return _mapper.Map<TCreateDto>(entity);
    }

    public virtual Task PreInsertAsync(TEntity entity) => Task.CompletedTask;
    public virtual Task PostInsertAsync(TEntity entity) => Task.CompletedTask;
}