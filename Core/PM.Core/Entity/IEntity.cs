namespace PM.Core.Entity;

public interface IEntity<Key>
{
    public Key Id { get; }
}