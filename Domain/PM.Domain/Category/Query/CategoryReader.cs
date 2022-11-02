using PM.Core.Database.UnitOfWork;
using PM.Domain.Category.Repository;

namespace PM.Domain.Category.Query;

public class CategoryReader
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryReader(IUnitOfWorkFactory unitOfWorkFactory, ICategoryRepository categoryRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Model.Category>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        await using var unitOfWork = _unitOfWorkFactory.Create();

        return await _categoryRepository.GetCategoriesAsync(unitOfWork, cancellationToken);
    }
}