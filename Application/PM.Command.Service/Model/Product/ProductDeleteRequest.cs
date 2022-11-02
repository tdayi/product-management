using System.ComponentModel.DataAnnotations;

namespace PM.Command.Service.Model.Product;

public class ProductDeleteRequest : IValidatableObject
{
    public Guid ProductId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Guid.Empty.Equals(ProductId))
        {
            yield return new ValidationResult("ProductId invalid");
        }
    }
}