using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity;

public class AuthOptions
{
    private readonly string _issuer;
    private readonly string _audience;
    private readonly string _key;
    private readonly int _expires;

    public AuthOptions(string issuer, string audience, string key, int expires = 5)
    {
        _issuer = issuer;
        _audience = audience;
        _key = key;
        _expires = expires;
    }

    public string Issuer => _issuer;
    public string Audience => _audience;
    public SymmetricSecurityKey Key => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
    public int Expires => _expires;

    public TokenValidationParameters TokenValidationParameters()
        => new() {
            ValidateIssuer = true,
            ValidIssuer = _issuer,
            ValidateAudience = true,
            ValidAudience = _audience,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key))
        };
    
}