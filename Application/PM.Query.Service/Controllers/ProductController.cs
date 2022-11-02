using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PM.Domain.Product.Model;
using PM.Domain.Product.Product;
using PM.Query.Service.Model.Product;

namespace PM.Query.Service.Controllers;

public class ProductController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ProductReader _productReader;

    public ProductController(IMapper mapper, ProductReader productReader)
    {
        _mapper = mapper;
        _productReader = productReader;
    }

    [HttpGet("/api/product/search")]
    public async Task<IActionResult> GetProductSearchAsync([FromQuery] ProductSearchRequest request,
        CancellationToken cancellationToken)
    {
        var products = (await _productReader.GetProductSearchAsync(request.Title, request.Description,
            request.CategoryName, request.MinStock, request.MaxStock, cancellationToken)).ToList();

        if (!products.Any())
        {
            return NotFound();
        }

        var results = _mapper.Map<List<ProductSearch>, List<ProductSearchModel>>(products);

        return Ok(results);
    }
}