using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PM.Command.Service.Model.Product;

namespace PM.Command.Service.Controllers;

public class ProductController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ProductController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("/api/product/save")]
    public async Task<IActionResult> ProductSaveAsync([FromBody] ProductSaveRequest request)
    {
        var productSaveCommand = request.ToCommand();

        await _publishEndpoint.Publish(productSaveCommand);

        return Accepted();
    }

    [HttpDelete("/api/product/delete")]
    public async Task<IActionResult> ProductDeleteAsync([FromBody] ProductSaveRequest request)
    {
        var productDeleteCommand = request.ToCommand();

        await _publishEndpoint.Publish(productDeleteCommand);

        return Accepted();
    }
}