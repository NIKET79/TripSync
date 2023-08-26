using Application.Common.Models;

namespace Application.MediatR.Authorization.Login;

public class LoginCommand: IRequest<Result>
{
    public string Email { get; set; }
    public string Password { get; set; }
}