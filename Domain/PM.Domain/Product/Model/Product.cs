using PM.Core.Entity;

namespace PM.Domain.Product.Model;

public class Product : IEntity<Guid>
{
    public Guid Id { get; protected set; }
    public string Title { get; protected set; }
    public string Description { get; protected set; }
    public int StockQuantity { get; protected set; }
    public Guid? CategoryId { get; protected set; }
    public Category.Model.Category Category { get; protected set; }

    public Product()
    {
    }

    public Product(string title, string description, int stockQuantity, Category.Model.Category category)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        StockQuantity = stockQuantity;
        Category = category;
    }
}