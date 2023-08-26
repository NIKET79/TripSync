using Application.Common.Models;
using Application.MediatR.Authorization.Login;
using Application.MediatR.Authorization.Register;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController : ApiControllerBase
{
    [HttpPost, Route("register")]
    public async Task<ActionResult<Result>> Register([FromBody] RegisterCommand command)
        => await Mediator.Send(command);

    [HttpPost, Route("login")]
    public async Task<ActionResult<Result>> Login([FromBody] LoginCommand command)
        => await Mediator.Send(command);
    
}