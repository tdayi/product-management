namespace PM.Domain.Product.Command;

public class ProductSaveCommand
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    public Guid? CategoryId { get; set; }
}