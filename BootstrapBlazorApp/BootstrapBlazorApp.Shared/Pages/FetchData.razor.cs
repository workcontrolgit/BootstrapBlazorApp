using BootstrapBlazorApp.Shared.Data;

namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class FetchData
    {
        private WeatherForecast[]? forecasts;
        protected override async Task OnInitializedAsync()
        {
            forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
        }
    }
}