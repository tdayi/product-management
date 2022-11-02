using System.ComponentModel.DataAnnotations;
using PM.Domain.Category.Command;

namespace PM.Command.Service.Model.Category;

public class CategorySaveRequest
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(200, ErrorMessage = "Name can be less than or equal 50 char")]
    public string Name { get; set; }

    [StringLength(500, ErrorMessage = "Description can be less than or equal 500 char")]
    public string Description { get; set; }

    public int MinStockQuantity { get; set; }

    public CategorySaveCommand ToCommand() => new()
    {
        Name = Name,
        Description = Description,
        MinStockQuantity = MinStockQuantity
    };

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        return ArraySegment<ValidationResult>.Empty;
    }
}