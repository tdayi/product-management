using MassTransit;
using PM.Core.Database.UnitOfWork;
using PM.Domain.Category.Command;
using PM.Domain.Category.Repository;

namespace PM.Domain.Category.CommandHandler;

public class CategorySaveCommandHandler : IConsumer<CategorySaveCommand>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ICategoryRepository _categoryRepository;

    public CategorySaveCommandHandler(IUnitOfWorkFactory unitOfWorkFactory, ICategoryRepository categoryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _categoryRepository = categoryRepository;
    }

    public async Task Consume(ConsumeContext<CategorySaveCommand> context)
    {
        await using var unitOfWork = _unitOfWorkFactory.Create();

        var category = new Model.Category(context.Message.Name, context.Message.Description,
            context.Message.MinStockQuantity);

        await _categoryRepository.InsertAsync(unitOfWork, category, context.CancellationToken);
    }
}