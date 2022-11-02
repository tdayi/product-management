using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PM.Domain.Category.Model;
using PM.Domain.Category.Query;
using PM.Query.Service.Model.Category;

namespace PM.Query.Service.Controllers;

public class CategoryController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly CategoryReader _categoryReader;

    public CategoryController(IMapper mapper, CategoryReader categoryReader)
    {
        _mapper = mapper;
        _categoryReader = categoryReader;
    }

    [HttpGet("/api/categories")]
    public async Task<IActionResult> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var categories = (await _categoryReader.GetCategoriesAsync(cancellationToken)).ToList();
        if (!categories.Any())
        {
            return NotFound();
        }

        var results = _mapper.Map<List<Category>, List<CategoryModel>>(categories);

        return Ok(results);
    }
}