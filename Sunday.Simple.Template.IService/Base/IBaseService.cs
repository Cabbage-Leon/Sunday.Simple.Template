using System.Linq.Expressions;

namespace Sunday.Simple.Template.IService.Base;

public interface IBaseService<TEntity, TVo> where TEntity : class
{
   Task<long> Add(TEntity entity);
   Task<List<long>> AddSplit(TEntity entity);
   Task<List<TVo>> Query(Expression<Func<TEntity, bool>>? whereExpression = null);
   Task<List<TEntity>> QuerySplit(Expression<Func<TEntity, bool>> whereExpression, string? orderByFields = null);
   Task<List<TVo>> QueryWithCache(Expression<Func<TEntity, bool>>? whereExpression = null);
}