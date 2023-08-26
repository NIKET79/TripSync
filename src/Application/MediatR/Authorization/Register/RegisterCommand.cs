using Application.Common.Models;

namespace Application.MediatR.Authorization.Register;

public class RegisterCommand : IRequest<Result>
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}