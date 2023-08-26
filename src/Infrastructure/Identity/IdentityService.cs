using Application.Common.Interfaces;
using Application.Common.Models;
using Ardalis.GuardClauses;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly AuthOptions _authOptions;

    public IdentityService(UserManager<User> userManager,
        ITokenService tokenService,
        IHttpContextAccessor contextAccessor,
        AuthOptions authOptions)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _contextAccessor = contextAccessor;
        _authOptions = authOptions;
    }
    
    public async Task<Guid> GetIdFromEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user?.Id ?? Guid.Empty;
    }

    public async Task<bool> IsInRoleAsync(Guid id, string role)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null) throw new NotFoundException("user", id.ToString());
        return await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(Guid userId, string policyName)
    {
        return true;
    }

    public async Task<Result> LoginAsync(Guid userId, string password)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null) throw new NotFoundException("user", userId.ToString());

        var token = await _tokenService.GetTokenAsync(user.Id);
        return !string.IsNullOrEmpty(token) ? 
            Result.Success(token) : 
            Result.Failure("Error while creating the token");
    }

    public async Task<Result> CreateUserAsync(string email, string username, string password)
    {
        var user = new User()
        {
            Email = email,
            UserName = username,
            FirstName = string.Empty,
            LastName = string.Empty
        };

        var createUser = await _userManager.CreateAsync(user, password);

        return createUser.Succeeded ? 
            Result.Success() : 
            Result.Failure(createUser.Errors.Select(er => er.Description));
    }

    public async Task<Result> DeleteUserAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null) throw new NotFoundException("user", userId.ToString());
        var deleteUser = await _userManager.DeleteAsync(user);
        
        return deleteUser.Succeeded ? Result.Success() : Result.Failure(deleteUser.Errors.Select(er => er.Description));
    }
}