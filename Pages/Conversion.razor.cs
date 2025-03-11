using ConvertisseurdeDevises.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvertisseurdeDevises.Pages
{
    public partial class Conversion : ComponentBase
    {
        [Inject]
        public ExchangeRateService ExchangeRateService { get; set; }

        [Inject]
        public FavoriteService FavoriteService { get; set; }

        private decimal amount = 1;
        private string fromCurrency = "USD";
        private string toCurrency = "EUR";
        private decimal? conversionResult;
        private List<string> allCurrencies = new();
        private List<string> favorites = new();
        private bool showFavoritesOnly = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadInitialData();
        }

        private async Task LoadInitialData()
        {
            var response = await ExchangeRateService.GetLatestRatesAsync("USD");
            if (response != null)
            {
                allCurrencies = new List<string>(response.Conversion_Rates.Keys);
            }
            favorites = await FavoriteService.GetFavoritesAsync();
        }

        private IEnumerable<string> FilteredCurrencies()
        {
            if (showFavoritesOnly)
            {
                return allCurrencies.Where(currency => favorites.Contains(currency));
            }
            return allCurrencies;
        }

        private void ToggleView()
        {
            showFavoritesOnly = !showFavoritesOnly;
        }

        private async Task Convert()
        {
            var responseFrom = await ExchangeRateService.GetLatestRatesAsync(fromCurrency);
            
            if (responseFrom != null && responseFrom.Conversion_Rates.ContainsKey(toCurrency))
            {
                conversionResult = amount * responseFrom.Conversion_Rates[toCurrency];
            }
            else
            {
                conversionResult = null;
            }
            
            StateHasChanged();
        }
    }
}
