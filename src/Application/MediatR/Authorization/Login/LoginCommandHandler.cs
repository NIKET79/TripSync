using Application.Common.Interfaces;
using Application.Common.Models;
using NotFoundException = Application.Common.Exceptions.NotFoundException;

namespace Application.MediatR.Authorization.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var id = await _identityService.GetIdFromEmailAsync(request.Email);
        if (id != Guid.Empty) return await _identityService.LoginAsync(id, request.Password);
        throw new NotFoundException("user", request.Email);
    }
}