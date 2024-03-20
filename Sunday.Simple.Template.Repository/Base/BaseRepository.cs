using Sunday.Simple.Template.Entity;
using System.Linq.Expressions;

namespace Sunday.Simple.Template.Repository.Base;

public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
{
    private readonly EfContext _efContext;

    public BaseRepository(EfContext efContext)
    {
        _efContext = efContext;
    }

    public async Task Add(T entity)
    {
        await _efContext.Set<T>().AddAsync(entity);
    }

    public Task<List<long>> AddSplit(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> Query(Expression<Func<T, bool>> whereExpression)
    {
        throw new NotImplementedException();
    }

    public Task<List<TResult>> QueryMuch<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, object[]>> joinExpression, Expression<Func<T1, T2, T3, TResult>> selectExpression, Expression<Func<T1, T2, T3, bool>> whereLambda = default) where T1 : class, new()
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> QuerySplit(Expression<Func<T, bool>> whereExpression, string orderByFields = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> QueryWithCache(Expression<Func<T, bool>> whereExpression = null)
    {
        throw new NotImplementedException();
    }
}