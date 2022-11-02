using System.ComponentModel.DataAnnotations;
using PM.Domain.Product.Command;

namespace PM.Command.Service.Model.Product;

public class ProductSaveRequest : IValidatableObject
{
    public Guid? CategoryId { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title can be less than or equal 200 char")]
    public string Title { get; set; }

    [StringLength(500, ErrorMessage = "Description can be less than or equal 500 char")]
    public string Description { get; set; }

    public int StockQuantity { get; set; }

    public ProductSaveCommand ToCommand() => new()
    {
        CategoryId = CategoryId,
        Title = Title,
        Description = Description,
        StockQuantity = StockQuantity
    };

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (CategoryId is not null && Guid.Empty.Equals(CategoryId.Value))
        {
            yield return new ValidationResult("CategoryId invalid");
        }
    }
}