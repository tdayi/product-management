using PM.Core.Database.Repository;
using PM.Core.Database.UnitOfWork;

namespace PM.Domain.Product.Repository;

public interface IProductRepository : IRepository<Model.Product, Guid>
{
    Task<IEnumerable<Model.ProductSearch>> GetProductSearchAsync(IUnitOfWork unitOfWork, string title, string description,
        string categoryName, int? minStock, int? maxStock, CancellationToken cancellationToken);
}