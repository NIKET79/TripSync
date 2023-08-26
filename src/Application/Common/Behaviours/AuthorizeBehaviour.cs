using System.Reflection;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;

namespace Application.Common.Behaviours;

public class AuthorizeBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest: notnull
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public AuthorizeBehaviour(
        ICurrentUserService currentUserService,
        IIdentityService identityService)
    {
        _currentUserService = currentUserService;
        _identityService = identityService;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>().ToList();

        if (authorizeAttributes.Count == 0) return await next();
        
        // Must be authenticated user
        if (_currentUserService.Id == Guid.Empty)
        {
            throw new UnauthorizedAccessException();
        }

        // Role-based authorization
        var authorizeAttributesWithRoles = 
            authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles)).ToList();

        if (authorizeAttributesWithRoles.Count > 0)
        {
            var authorized = false;

            foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
            {
                foreach (var role in roles)
                {
                    var isInRole = await _identityService.IsInRoleAsync(_currentUserService.Id, role.Trim());
                    if (!isInRole) continue;
                    authorized = true;
                    break;
                }
            }
            
            if (!authorized) throw new ForbiddenAccessException();
        }

        // Policy-based authorization
        var authorizeAttributesWithPolicies = 
            authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy)).ToList();
        if (authorizeAttributesWithPolicies.Count <= 0) return await next();
        
        foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
        {
            var authorized = await _identityService.AuthorizeAsync(_currentUserService.Id, policy);
            
            if (!authorized)
            {
                throw new ForbiddenAccessException();
            }
        }
        
        // User is authorized / authorization not required
        return await next();
    }
}