namespace PM.Query.Service.Model.Product;

public class ProductSearchModel
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int CategoryMinStockQuantity { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
}