using System.Linq.Expressions;
using AutoMapper;
using Sunday.Simple.Template.IService.Base;
using Sunday.Simple.Template.Repository.Base;

namespace Sunday.Simple.Template.Service.Base;

public class BaseServices<TEntity, TVo> : IBaseService<TEntity, TVo> where TEntity : class, new()
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<TEntity> _baseRepository;

    protected BaseServices(IMapper mapper, IBaseRepository<TEntity> baseRepository)
    {
        _mapper = mapper;
        _baseRepository = baseRepository;
    }

    public async Task<List<TVo>> Query(Expression<Func<TEntity, bool>>? whereExpression = null)
    {
        var entities = await _baseRepository.Query(whereExpression);
        return _mapper.Map<List<TVo>>(entities);
    }

    public async Task<List<TVo>> QueryWithCache(Expression<Func<TEntity, bool>>? whereExpression = null)
    {
        var entities = await _baseRepository.QueryWithCache(whereExpression);
        return _mapper.Map<List<TVo>>(entities);
    }

    public async Task Add(TEntity entity)
    {
        await _baseRepository.Add(entity);
    }

    public async Task<List<TEntity>> QuerySplit(Expression<Func<TEntity, bool>> whereExpression,
        string? orderByFields)
    {
        return await _baseRepository.QuerySplit(whereExpression, orderByFields);
    }

    public async Task<List<long>> AddSplit(TEntity entity)
    {
        return await _baseRepository.AddSplit(entity);
    }
}