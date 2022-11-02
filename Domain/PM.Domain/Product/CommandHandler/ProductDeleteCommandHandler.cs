using MassTransit;
using PM.Core.Database.UnitOfWork;
using PM.Domain.Product.Command;
using PM.Domain.Product.Repository;

namespace PM.Domain.Product.CommandHandler;

public class ProductDeleteCommandHandler : IConsumer<ProductDeleteCommand>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IProductRepository _productRepository;

    public ProductDeleteCommandHandler(IUnitOfWorkFactory unitOfWorkFactory, IProductRepository productRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<ProductDeleteCommand> context)
    {
        await using var unitOfWork = _unitOfWorkFactory.Create();

        var product = await _productRepository.GetByKeyAsync(unitOfWork, context.Message.ProductId,
            context.CancellationToken);

        if (product is null)
        {
            throw new ArgumentNullException($"{nameof(product)} key: {context.Message.ProductId}");
        }

        await _productRepository.DeleteAsync(unitOfWork, product, context.CancellationToken);
    }
}