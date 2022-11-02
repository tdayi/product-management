using System.ComponentModel.DataAnnotations;

namespace PM.Domain.Product.Command;

public class ProductSaveCommand : IValidatableObject
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    public Guid? CategoryId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            yield return new ValidationResult("Title is required");
        }

        if (Title?.Length > 200)
        {
            yield return new ValidationResult("Title can be less than or equal 200 char");
        }

        if (Description?.Length > 500)
        {
            yield return new ValidationResult("Description can be less than or equal 500 char");
        }

        if (CategoryId is not null && Guid.Empty.Equals(CategoryId.Value))
        {
            yield return new ValidationResult("CategoryId invalid");
        }
    }
}