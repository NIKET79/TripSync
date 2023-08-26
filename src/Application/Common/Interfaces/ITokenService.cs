namespace Application.Common.Interfaces;

public interface ITokenService
{
    Task<string?> GetTokenAsync(Guid userId);
}