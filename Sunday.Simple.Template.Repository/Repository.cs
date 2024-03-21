using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sunday.Simple.Template.Entity;
using Sunday.Simple.Template.Repository.Base;

namespace Sunday.Simple.Template.Repository;

public class Repository<TEntity>(DbContext dbDbContext) : Repository<TEntity, string>(dbDbContext), IRepository<TEntity>
    where TEntity : class, IEntity;

public class Repository<TEntity, TPrimaryKey>(DbContext context) : RepositoryBase<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    protected virtual DbSet<TEntity> Table
    {
        get { return context.Set<TEntity>(); }
    }

    public override IQueryable<TEntity> Query()
    {
        return Table.AsQueryable();
    }

    public override IQueryable<TEntity> QueryNoTracking()
    {
        return Table.AsQueryable().AsNoTracking();
    }

    public override TEntity Insert(TEntity entity)
    {
        var newEntity = Table.Add(entity).Entity;
        return newEntity;
    }

    public override async Task<TEntity> InsertAsync(TEntity entity)
    {
        var entityEntry = await Table.AddAsync(entity);
        return entityEntry.Entity;
    }

    public override void Insert(List<TEntity> entities)
    {
        Table.AddRange(entities);
    }

    public override Task InsertAsync(List<TEntity> entities)
    {
        Table.AddRangeAsync(entities);
        return Task.CompletedTask;
    }

    public override TEntity Update(TEntity entity)
    {
        AttachIfNot(entity);
        context.Entry(entity).State = EntityState.Modified;
        return entity;
    }

    public override void Update(List<TEntity> entities)
    {
        Table.UpdateRange(entities);
    }

    public override Task UpdateAsync(List<TEntity> entities)
    {
        Table.UpdateRange(entities);
        return Task.CompletedTask;
    }

    public override void Delete(TEntity entity)
    {
        if (entity != null)
        {
            entity.UpdateIsDelete();
            entity.UpdateModifyTime();

            Update(entity);
        }
    }

    public override void Delete(TPrimaryKey id)
    {
        var entity = Get(id);
        Delete(entity);
    }

    public override void Delete(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = GetAll(predicate);
        if (entities.Any())
        {
            entities.ForEach(entity => { Delete(entity); });
        }
    }

    public override void HardDelete(TEntity entity)
    {
        AttachIfNot(entity);
        Table.Remove(entity);
    }

    public override void HardDelete(TPrimaryKey id)
    {
        var entity = GetFromChangeTrackerOrNull(id);
        if (entity != null)
        {
            HardDelete(entity);
            return;
        }

        entity = Get(id);
        if (entity != null)
        {
            HardDelete(entity);
            return;
        }
    }

    public override void HardDelete(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = Table.Where(predicate).ToList();
        if (entities.Any())
        {
            entities.ForEach(entity => { AttachIfNot(entity); });
            Table.RemoveRange(entities);
        }
    }

    protected virtual void AttachIfNot(TEntity entity)
    {
        var entry = context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
        if (entry != null)
        {
            return;
        }

        Table.Attach(entity);
    }

    private TEntity GetFromChangeTrackerOrNull(TPrimaryKey id)
    {
        var entry = context.ChangeTracker.Entries()
            .FirstOrDefault(
                ent =>
                    ent.Entity is TEntity &&
                    EqualityComparer<TPrimaryKey>.Default.Equals(id, ((TEntity)ent.Entity).Id)
            );

        return entry?.Entity as TEntity;
    }
}