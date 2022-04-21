using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using repository;
using MudBlazor;
using service;
using System.Globalization;
using Microsoft.Extensions.Localization;

namespace nintens.Pages
{
    public partial class UserProfile
    {
        [Inject]
        IHttpContextAccessor HttpContextAccessor { get; set; }

        [Inject]
        UserManager<ApplicationUser> _UserManager { get; set; }

        [Inject]
        ApplicationIdentityService service { get; set; }

        [Inject]
        ILogger<UserProfile> logger { get; set; }

        [Inject]
        NavigationManager Nav { get;set ; }



        private ApplicationUser curUser;

        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            curUser = await service.getUser(HttpContextAccessor.HttpContext.User.Identity.Name);
            Culture = CultureInfo.CurrentCulture;
            StateHasChanged();
        }

        private async void Save()
        {
            await service.updateTimeZone(curUser);
            Snackbar.Add($"User Profile is updated successfully!", Severity.Info);
            StateHasChanged();
        }

        private CultureInfo[] supportedCultures = new[]
        {
            new CultureInfo("en-US"),
            new CultureInfo("zh-CN"),
        };

        private CultureInfo Culture
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                if (CultureInfo.CurrentCulture != value)
                {
                    var uri = new Uri(Nav.Uri)
                        .GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
                    var cultureEscaped = Uri.EscapeDataString(value.Name);
                    var uriEscaped = Uri.EscapeDataString(uri);

                    Nav.NavigateTo(
                        $"api/Culture/Set?culture={cultureEscaped}&redirectUri={uriEscaped}",
                        forceLoad: true);
                }
            }
        }
    }
}
