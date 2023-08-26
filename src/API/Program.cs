using System.Globalization;
using API;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureApplicationCookie(cookie =>
{
    cookie.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebUi();

var app = builder.Build();

var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCookiePolicy(new CookiePolicyOptions
{
    Secure = CookieSecurePolicy.Always,
    HttpOnly = HttpOnlyPolicy.Always
});
app.UseCors(cfg =>
{
    cfg.WithOrigins("https://localhost:4200/")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(_ => true);
});

app.MapControllers();

app.Run();