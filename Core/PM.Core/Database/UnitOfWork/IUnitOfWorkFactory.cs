namespace PM.Core.Database.UnitOfWork;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create();
}