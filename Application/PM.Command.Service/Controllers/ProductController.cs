using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PM.Command.Service.ActionFilter;
using PM.Command.Service.Model.Product;

namespace PM.Command.Service.Controllers;

public class ProductController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ProductController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [RequestValidation]
    [HttpPost("/api/product/save")]
    public async Task<IActionResult> ProductSaveAsync([FromBody] ProductSaveRequest request,
        CancellationToken cancellationToken)
    {
        var productSaveCommand = request.ToCommand();

        await _publishEndpoint.Publish(productSaveCommand, cancellationToken);

        return Accepted();
    }

    [RequestValidation]
    [HttpDelete("/api/product/delete")]
    public async Task<IActionResult> ProductDeleteAsync([FromBody] ProductDeleteRequest request,
        CancellationToken cancellationToken)
    {
        var productDeleteCommand = request.ToCommand();

        await _publishEndpoint.Publish(productDeleteCommand, cancellationToken);

        return Accepted();
    }
}