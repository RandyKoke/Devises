// Services/FavoriteService.cs
using Blazored.LocalStorage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConvertisseurdeDevises.Services
{
    public class FavoriteService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "favoriteCurrencies";

        public FavoriteService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<string>> GetFavoritesAsync()
        {
            return await _localStorage.GetItemAsync<List<string>>(StorageKey) ?? new List<string>();
        }

        public async Task AddFavoriteAsync(string currency)
        {
            var favorites = await GetFavoritesAsync();
            if (!favorites.Contains(currency))
            {
                favorites.Add(currency);
                await _localStorage.SetItemAsync(StorageKey, favorites);
            }
        }

        public async Task RemoveFavoriteAsync(string currency)
        {
            var favorites = await GetFavoritesAsync();
            if (favorites.Contains(currency))
            {
                favorites.Remove(currency);
                await _localStorage.SetItemAsync(StorageKey, favorites);
            }
        }
    }
}