using PM.Core.Database.Repository;

namespace PM.Domain.Product.Repository;

public interface IProductRepository : IRepository<Model.Product, Guid>
{
    
}