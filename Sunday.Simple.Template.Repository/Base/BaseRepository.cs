using System.Linq.Expressions;

namespace Sunday.Simple.Template.Repository.Base;

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        public BaseRepository()
        {
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>>? whereExpression = null)
        {
            await Console.Out.WriteLineAsync(Db.GetHashCode().ToString());
            return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        public async Task<List<TEntity>> QueryWithCache(Expression<Func<TEntity, bool>>? whereExpression = null)
        {
            return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).WithCache().ToListAsync();
        }

        public async Task<long> Add(TEntity entity)
        {
            var insert = _db.Insertable(entity);
            return await insert.ExecuteReturnSnowflakeIdAsync();
        }

        /// <summary>
        /// 分表查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderByFields">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> QuerySplit(Expression<Func<TEntity, bool>> whereExpression, string? orderByFields = null)
        {
            return await _db.Queryable<TEntity>()
                .SplitTable()
                .OrderByIF(!string.IsNullOrEmpty(orderByFields), orderByFields)
                .WhereIF(whereExpression != null, whereExpression)
                .ToListAsync();
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns></returns>
        public async Task<List<long>> AddSplit(TEntity entity)
        {
            var insert = _db.Insertable(entity).SplitTable();
            //插入并返回雪花ID并且自动赋值ID　
            return await insert.ExecuteReturnSnowflakeIdListAsync();
        }
        public async Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(
      Expression<Func<T, T2, T3, object[]>> joinExpression,
      Expression<Func<T, T2, T3, TResult>> selectExpression,
      Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new()
        {
            if (whereLambda == null)
            {
                return await _db.Queryable(joinExpression).Select(selectExpression).ToListAsync();
            }

            return await _db.Queryable(joinExpression).Where(whereLambda).Select(selectExpression).ToListAsync();
        }
    }