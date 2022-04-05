using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using repository;
using service;

using MudBlazor;
using MudBlazor.Services;

namespace nintens
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly string connString;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            connString = Configuration.GetConnectionString("Product");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection()
               .SetApplicationName("nintens")
               .PersistKeysToFileSystem(new DirectoryInfo(@"/var/aspnetkeys/")); ;

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddDbContextFactory<nintensdbcontext>(options => options.UseMySql(
                 connString,
                ServerVersion.AutoDetect(connString)));

            services.AddTransient<WeatherForecastService>();

            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;

                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });

        }
    }
}
