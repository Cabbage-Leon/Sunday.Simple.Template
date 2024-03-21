namespace Sunday.Simple.Template.Entity;

public interface IEntity : IEntity<string>
{
}

public interface IEntity<TPrimaryKey> : ITrack
{
    TPrimaryKey Id { get; }
}