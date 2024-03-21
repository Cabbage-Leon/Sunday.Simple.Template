namespace Sunday.Simple.Template.Entity;

public abstract class Entity : Entity<string>, IEntity
{
}

public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    public TPrimaryKey Id { get; protected set; } = default;
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; protected set; }
    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; protected set; }
    /// <summary>
    /// 是否删除
    /// </summary>
    public bool IsDelete { get; protected set; }

    public void UpdateCreateTime()
    {
        CreateTime = DateTime.Now;
    }

    public void UpdateModifyTime()
    {
        UpdateTime = DateTime.Now;
    }

    public void UpdateIsDelete()
    {
        IsDelete = true;
    }

}