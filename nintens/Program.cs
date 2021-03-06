using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Hosting;
using nintens;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using service;
using Serilog;
using Serilog.Context;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using repository;

var builder = WebApplication.CreateBuilder(args);

// Manually create an instance of the Startup class
var startup = new Startup(builder.Configuration);
// Manually call ConfigureServices()
startup.ConfigureServices(builder.Services);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//builder.Services.AddSingleton<WeatherForecastService>();

builder.Configuration.AddEnvironmentVariables().AddUserSecrets<Startup>(optional: true, reloadOnChange: true);

//add Serilog Config begin
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
Serilog.Debugging.SelfLog.Enable(Console.Error);
builder.Host.UseSerilog();
//add Serilog Config end


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NintensRestfulApi v1"));

app.UseStaticFiles();

//Suport I18N begin
var supportedCultures = new[] { "en-US", "zh-CN" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
var questStringCultureProvider = localizationOptions.RequestCultureProviders[0];
localizationOptions.RequestCultureProviders.RemoveAt(0);
localizationOptions.RequestCultureProviders.Insert(1, questStringCultureProvider);

app.UseRequestLocalization(localizationOptions);
//Suport I18N end

app.UseRouting();

//Add Authorization Begin
//app.MapBlazorHub();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages().RequireAuthorization();
app.MapBlazorHub().RequireAuthorization();
//Add Authorization End

//Add WebAPI Contoller Access Begin
app.MapControllers();
//Add WebAPI Contoller Access End


app.MapFallbackToPage("/_Host");

//Config logContext Begin
app.Use(async (httpContext, next) =>
{
    //Get username  
    var username = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : "anonymous";
    LogContext.PushProperty("User", username);

    //Get remote IP address  
    var ip = httpContext.Connection.RemoteIpAddress.ToString();
    LogContext.PushProperty("IP", !String.IsNullOrWhiteSpace(ip) ? ip : "unknown");

    await next.Invoke();
});
//Config logContext End

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/Identity/Account/Manage/", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
    endpoints.MapGet("/Identity/Account/Manage/email", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
    endpoints.MapGet("/Identity/Account/Manage/changepassword", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
    endpoints.MapGet("/Identity/Account/Manage/personaldata", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
    endpoints.MapGet("/Identity/Account/Manage/twofactorauthentication", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
    endpoints.MapPost("/Identity/Account/Manage/", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
    endpoints.MapPost("/Identity/Account/Manage/email", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
    endpoints.MapPost("/Identity/Account/Manage/changepassword", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
    endpoints.MapPost("/Identity/Account/Manage/personaldata", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
    endpoints.MapPost("/Identity/Account/Manage/twofactorauthentication", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
});

app.Run();
