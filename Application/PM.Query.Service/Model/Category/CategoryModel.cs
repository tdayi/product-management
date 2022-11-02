namespace PM.Query.Service.Model.Category;

public class CategoryModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int MinStockQuantity { get; set; }
}