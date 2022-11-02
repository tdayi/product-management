using Microsoft.EntityFrameworkCore;
using PM.Core.Database.PostgreSql.Repository;
using PM.Core.Database.UnitOfWork;
using PM.Domain.Product.Model;
using PM.Domain.Product.Repository;
using PM.Domain.Repository.Context;

namespace PM.Domain.Repository.Concrete;

public class ProductRepository : Repository<Domain.Product.Model.Product, Guid>,
    IProductRepository
{
    public async Task<IEnumerable<ProductSearch>> GetProductSearchAsync(IUnitOfWork unitOfWork, string title,
        string description, string categoryName, int? minStock, int? maxStock, CancellationToken cancellationToken)
    {
        var context = unitOfWork.DbSession<PMDbContext>();

        var query = (from product in context.Product
            join category in context.Category on product.Category.Id equals category.Id
            where product.StockQuantity > category.MinStockQuantity
            select new ProductSearch
            {
                Id = product.Id,
                CategoryId = category.Id,
                CategoryName = category.Name,
                CategoryMinStockQuantity = category.MinStockQuantity,
                Description = product.Description,
                StockQuantity = product.StockQuantity,
                Title = product.Title
            });

        if (!string.IsNullOrWhiteSpace(title?.Trim()))
        {
            query = (from q in query where q.Title.Contains(title) select q);
        }

        if (!string.IsNullOrWhiteSpace(description?.Trim()))
        {
            query = (from q in query where q.Description.Contains(description) select q);
        }

        if (!string.IsNullOrWhiteSpace(categoryName?.Trim()))
        {
            query = (from q in query where q.CategoryName.Contains(categoryName) select q);
        }

        if (minStock.HasValue)
        {
            query = (from q in query where q.StockQuantity >= minStock.Value select q);
        }

        if (maxStock.HasValue)
        {
            query = (from q in query where q.StockQuantity <= maxStock.Value select q);
        }

        return await query.ToListAsync(cancellationToken);
    }
}