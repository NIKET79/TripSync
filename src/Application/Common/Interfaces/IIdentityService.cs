using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Guid> GetIdFromEmailAsync(string email);
    Task<bool> IsInRoleAsync(Guid id, string role);
    Task<bool> AuthorizeAsync(Guid userId, string policyName);
    Task<Result> LoginAsync(Guid userId, string password);
    Task<Result> CreateUserAsync(string email, string username, string password);
    Task<Result> DeleteUserAsync(Guid userId);
}