using System.Net.Http.Json;
using ConvertisseurdeDevises.Models;

namespace ConvertisseurdeDevises.Services;

public class ExchangeRateService
{
    private readonly HttpClient _httpClient;
    
    public ExchangeRateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ExchangeRateResponse?> GetLatestRatesAsync(string baseCurrency)
    {
        try
        {
            var url = $"https://v6.exchangerate-api.com/v6/929975ecf719ec3950979860/latest/{baseCurrency}";
            return await _httpClient.GetFromJsonAsync<ExchangeRateResponse>(url);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching exchange rates: {ex.Message}");
            return null;
        }
    }
}