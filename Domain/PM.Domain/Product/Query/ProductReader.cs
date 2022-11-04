using PM.Core.Database.UnitOfWork;
using PM.Domain.Product.Repository;

namespace PM.Domain.Product.Query;

public class ProductReader
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IProductRepository _productRepository;

    public ProductReader(IUnitOfWorkFactory unitOfWorkFactory, IProductRepository productRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Model.ProductSearch>> GetProductSearchAsync(string title, string description,
        string categoryName, int? minStock, int? maxStock, CancellationToken cancellationToken)
    {
        await using var unitOfWork = _unitOfWorkFactory.Create();

        return await _productRepository.GetProductSearchAsync(unitOfWork, title, description, categoryName, minStock,
            maxStock, cancellationToken);
    }
}