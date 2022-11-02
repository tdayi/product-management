using PM.Core.Entity;

namespace PM.Domain.Category.Model;

public class Category : IEntity<Guid>
{
    public Guid Id { get; protected set; }
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public int MinStockQuantity { get; protected set; }
    public ICollection<Product.Model.Product> Products { get; protected set; }

    public Category()
    {
    }

    public Category(string name, string description, int minStockQuantity)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        MinStockQuantity = minStockQuantity;
    }
}