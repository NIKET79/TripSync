using API.Services;
using Application.Common.Interfaces;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddWebUi(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSignalR();
        services.AddHttpContextAccessor();
        return services;
    }

    public static IApplicationBuilder ConfigurePipeline(this IApplicationBuilder app)
    {
        
        
        return app;
    }
}