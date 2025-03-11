using ConvertisseurdeDevises.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvertisseurdeDevises.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public ExchangeRateService ExchangeRateService { get; set; }

        [Inject]
        public FavoriteService FavoriteService { get; set; }

        private string baseCurrency = "USD";
        private Dictionary<string, decimal> rates;
        private List<string> allCurrencies = new();
        private List<string> favorites = new();
        private bool showFavoritesOnly = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadInitialData();
        }

        private async Task LoadInitialData()
        {
            var response = await ExchangeRateService.GetLatestRatesAsync(baseCurrency);
            if (response != null)
            {
                allCurrencies = new List<string>(response.Conversion_Rates.Keys);
            }
            favorites = await FavoriteService.GetFavoritesAsync();
        }

        private async Task GetRates()
        {
            var response = await ExchangeRateService.GetLatestRatesAsync(baseCurrency);
            rates = response?.Conversion_Rates;
        }

        private IEnumerable<KeyValuePair<string, decimal>> FilteredRates()
        {
            if (showFavoritesOnly)
            {
                return rates?.Where(rate => IsFavorite(rate.Key)) ?? Enumerable.Empty<KeyValuePair<string, decimal>>();
            }
            return rates ?? Enumerable.Empty<KeyValuePair<string, decimal>>();
        }

        private void ToggleView()
        {
            showFavoritesOnly = !showFavoritesOnly;
        }

        private bool IsFavorite(string currency)
        {
            return favorites.Contains(currency);
        }

        private async Task ToggleFavorite(string currency)
        {
            if (IsFavorite(currency))
                await FavoriteService.RemoveFavoriteAsync(currency);
            else
                await FavoriteService.AddFavoriteAsync(currency);

            favorites = await FavoriteService.GetFavoritesAsync();
            StateHasChanged();
        }
    }
}
