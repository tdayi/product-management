using Microsoft.EntityFrameworkCore;
using PM.Core.Database.PostgreSql.Repository;
using PM.Core.Database.UnitOfWork;
using PM.Domain.Category.Repository;
using PM.Domain.Repository.Context;

namespace PM.Domain.Repository.Concrete;

public class CategoryRepository : Repository<Domain.Category.Model.Category, Guid>,
    ICategoryRepository
{
    public async Task<IEnumerable<Category.Model.Category>> GetCategoriesAsync(IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var context = unitOfWork.DbSession<PMDbContext>();

        return await context.Category.ToListAsync(cancellationToken);
    }
}