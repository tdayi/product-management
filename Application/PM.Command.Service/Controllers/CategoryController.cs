using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PM.Command.Service.ActionFilter;
using PM.Command.Service.Model.Category;

namespace PM.Command.Service.Controllers;

public class CategoryController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public CategoryController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [RequestValidation]
    [HttpPost("/api/category/save")]
    public async Task<IActionResult> CategorySaveAsync([FromBody] CategorySaveRequest request,
        CancellationToken cancellationToken)
    {
        var categorySaveCommand = request.ToCommand();

        await _publishEndpoint.Publish(categorySaveCommand, cancellationToken);

        return Accepted();
    }
}