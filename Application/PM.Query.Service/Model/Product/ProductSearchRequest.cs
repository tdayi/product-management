namespace PM.Query.Service.Model.Product;

public class ProductSearchRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public int? MinStock { get; set; }
    public int? MaxStock { get; set; }
}