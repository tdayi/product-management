using PM.Core.Database.PostgreSql.Repository;
using PM.Domain.Product.Repository;

namespace PM.Domain.Repository.Concrete;

public class ProductRepository : Repository<Domain.Product.Model.Product, Guid>,
    IProductRepository
{
    
}