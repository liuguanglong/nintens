using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Hosting;
using nintens;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using service;
using Serilog;
using Serilog.Context;

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


app.UseStaticFiles();

app.UseRouting();

//Add Authorization Begin
//app.MapBlazorHub();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages().RequireAuthorization();
app.MapBlazorHub().RequireAuthorization();
//Add Authorization End

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

app.Run();
