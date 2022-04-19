using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MudBlazor;
using modal;
using service;
using repository;

namespace nintens.Pages
{
    public partial class Systemlog
    {
        private MudTable<OperationLog> table;
        private string searchString = null;

        [Inject]
        IHttpContextAccessor HttpContextAccessor { get; set; }

        [Inject]
        ApplicationIdentityService userservice { get; set; }

        [Inject]
        WeatherForecastService service { get; set; }

        TimeZoneInfo zone = TimeZoneInfo.Local;
        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            var user = await userservice.getUser(HttpContextAccessor.HttpContext.User.Identity.Name);
            zone = TimeZoneInfo.FindSystemTimeZoneById(user.TimeZoneId);
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }

        private async Task<TableData<OperationLog>> ServerReload(TableState state)
        {
            Tuple<int, List<OperationLog>> ret = await service.getLogs(searchString, state.SortLabel, state.SortDirection == SortDirection.Descending, state.PageSize, state.Page);
            return new TableData<OperationLog>() { TotalItems = ret.Item1, Items = ret.Item2 };
        }

        public DateTimeOffset ToLocalDateTime(DateTime? dateTime)
        {
            DateTimeOffset utcTime = DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc);
            return utcTime.ToOffset(zone.BaseUtcOffset);
        }
    }
}
