using System.Globalization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace API.Middlewares;

public class LocalizationMiddleware
{
    private readonly RequestDelegate next;
    private readonly IOptions<RequestLocalizationOptions> _options;

    public LocalizationMiddleware(RequestDelegate next, IOptions<RequestLocalizationOptions> options)
    {
        this.next = next;
        _options = options;
    }
  
    public async Task InvokeAsync(HttpContext context)
    {
        var langSet = context.Request.Headers.TryGetValue("localize-language", out var value);

        foreach (var l in value.Select(lang => new CultureInfo(lang))
                     .Where(l => _options.Value.SupportedCultures != null && 
                                 _options.Value.SupportedCultures.Contains(l)))
        {
            CultureInfo.CurrentCulture = l;
        }
    }
}