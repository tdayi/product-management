using System.ComponentModel.DataAnnotations;
using MassTransit;
using PM.Core.Database.UnitOfWork;
using PM.Domain.Category.Repository;
using PM.Domain.Product.Command;
using PM.Domain.Product.Repository;

namespace PM.Domain.Product.CommandHandler;

public class ProductSaveCommandHandler : IConsumer<ProductSaveCommand>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductSaveCommandHandler(IUnitOfWorkFactory unitOfWorkFactory, IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task Consume(ConsumeContext<ProductSaveCommand> context)
    {
        var validationContext = new ValidationContext(context.Message);
        if (context.Message.Validate(validationContext).Any())
        {
            throw new InvalidDataException(nameof(context.Message));
        }

        Category.Model.Category category = null;

        await using var unitOfWork = _unitOfWorkFactory.Create();

        if (context.Message.CategoryId is not null && !Guid.Empty.Equals(context.Message.CategoryId))
        {
            category = await _categoryRepository.GetByKeyAsync(unitOfWork, context.Message.CategoryId.Value,
                context.CancellationToken);

            if (category is null)
            {
                throw new ArgumentNullException($"{nameof(category)} key: {context.Message.CategoryId}");
            }
        }

        var product = new Model.Product(context.Message.Title, context.Message.Description,
            context.Message.StockQuantity, category);

        await _productRepository.InsertAsync(unitOfWork, product, context.CancellationToken);
    }
}