using PM.Core.Database.Repository;
using PM.Core.Database.UnitOfWork;

namespace PM.Domain.Category.Repository;

public interface ICategoryRepository : IRepository<Model.Category, Guid>
{
    Task<IEnumerable<Domain.Category.Model.Category>>
        GetCategoriesAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken);
}