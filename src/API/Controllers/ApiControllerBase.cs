using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController, Route("api/[controller]")]
public class ApiControllerBase : Controller
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}