using System.IdentityModel.Tokens.Jwt;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity;

public class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
    private readonly AuthOptions _authOptions;

    public TokenService(UserManager<User> userManager,
        IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
        AuthOptions authOptions)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authOptions = authOptions;
    }
    
    public async Task<string?> GetTokenAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null) return string.Empty;

        var claimsPrincipal = await _userClaimsPrincipalFactory.CreateAsync(user);
        var credentials = new SigningCredentials(_authOptions.Key, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _authOptions.Issuer,
            audience: _authOptions.Audience,
            claims: claimsPrincipal.Claims,
            expires: DateTime.UtcNow.AddMinutes(_authOptions.Expires),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}