namespace PM.Domain.Category.Command;

public class CategorySaveCommand
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int MinStockQuantity { get; set; }
}