using System.Globalization;
using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Entities;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        var issuer = configuration["AUTH:JWT:ISSUER"];
        var audience = configuration["AUTH:JWT:AUDIENCE"];
        var key = configuration["AUTH:JWT:KEY"];
        var expires = int.TryParse(configuration["AUTH:JWT:EXPIRES"], out var expiration); 
        
        Guard.Against.Null(issuer, "Issuer is not defined");
        Guard.Against.Null(audience, "Audience is not defined");
        Guard.Against.Null(key, "Key is not defined");

        var authOptions = new AuthOptions(issuer, audience, key, expires ? expiration : 5);
        services.AddSingleton(authOptions);
        
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ITokenService, TokenService>();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("uk"),
                new CultureInfo("de")
            };
            options.DefaultRequestCulture = new RequestCulture("uk");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
        
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        
        services.AddDefaultIdentity<User>(cfg =>
            {
                cfg.Password.RequireDigit = true;
                cfg.Password.RequireLowercase = true;
                cfg.Password.RequireUppercase = true;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        
        return services;
    }
}