using PM.Core.Database.Repository;

namespace PM.Domain.Category.Repository;

public interface ICategoryRepository : IRepository<Model.Category, Guid>
{
    
}