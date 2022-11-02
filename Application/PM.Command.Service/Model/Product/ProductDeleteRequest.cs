using System.ComponentModel.DataAnnotations;
using PM.Domain.Product.Command;

namespace PM.Command.Service.Model.Product;

public class ProductDeleteRequest : IValidatableObject
{
    public Guid ProductId { get; set; }

    public ProductDeleteCommand ToCommand() => new()
    {
        ProductId = ProductId
    };

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Guid.Empty.Equals(ProductId))
        {
            yield return new ValidationResult("ProductId invalid");
        }
    }
}