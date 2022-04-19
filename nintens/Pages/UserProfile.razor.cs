using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using repository;
using MudBlazor;
using service;

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

        private ApplicationUser curUser;

        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            curUser = await service.getUser(HttpContextAccessor.HttpContext.User.Identity.Name);

            StateHasChanged();
        }

        private async void Save()
        {
            await service.updateUserTimeZone(curUser);
            Snackbar.Add($"User Profile is updated successfully!", Severity.Info);
            StateHasChanged();
        }
    }
}
