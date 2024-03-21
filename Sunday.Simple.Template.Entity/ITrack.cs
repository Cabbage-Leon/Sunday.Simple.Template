namespace Sunday.Simple.Template.Entity;

public interface ITrack
{
    /// <summary>
    /// 创建时间
    /// </summary>
    DateTime CreateTime { get; }
    /// <summary>
    /// 更新时间
    /// </summary>
    DateTime? UpdateTime { get; }
    /// <summary>
    /// 是否删除
    /// </summary>
    bool IsDelete { get; }
    /// <summary>
    /// 创建
    /// </summary>
    void UpdateCreateTime();
    /// <summary>
    /// 更新
    /// </summary>
    void UpdateModifyTime();
    /// <summary>
    /// 更新删除
    /// </summary>
    void UpdateIsDelete();
}