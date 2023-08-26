using Application.Common.Interfaces;
using Application.Common.Models;

namespace Application.MediatR.Authorization.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
{
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        => await _identityService.CreateUserAsync(request.Email, request.Username, request.Password);
}