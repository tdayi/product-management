using PM.Core.Database.PostgreSql.Repository;
using PM.Domain.Category.Repository;

namespace PM.Domain.Repository.Concrete;

public class CategoryRepository : Repository<Domain.Category.Model.Category, Guid>,
    ICategoryRepository
{
    
}