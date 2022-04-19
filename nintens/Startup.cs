using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using repository;
using service;

using MudBlazor;
using MudBlazor.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using PGoPlusTaskCenter.Areas.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using IdentityDemoWithSqlServr.Areas.Identity;
using Microsoft.OpenApi.Models;

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

            services.AddDbContextFactory<ApplicationDbContext>(options => options.UseMySql(
                 connString,
                ServerVersion.AutoDetect(connString)));

            services.AddTransient<IEmailSender, EmailSender>(i =>
                new EmailSender(
                    Configuration["EmailSender:Host"],
                    Configuration.GetValue<int>("EmailSender:Port"),
                    Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    Configuration["EmailSender:UserName"],
                    Configuration["EmailSender:Password"]
                )
            );

            //Add Identity Service Begin
            services.AddDbContextPool<ApplicationDbContext>(
                options => options.UseMySql(
                    connString,
                ServerVersion.AutoDetect(connString)));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedEmail = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
            services.AddAuthentication("Identity.Application")
                .AddCookie()
                .AddJwtBearerConfiguration(
                Configuration["Jwt:ValidIssuer"],
                Configuration["Jwt:ValidAudience"],
                Configuration["JWT:Secret"]
              );
            //Add Identity Service End

            services.AddTransient<WeatherForecastService>();
            services.AddTransient<ApplicationIdentityService>();

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

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "NintensRestfulApi", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
           });
        }
    }
}
