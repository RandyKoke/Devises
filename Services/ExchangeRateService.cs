using System.Net.Http.Json;

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
            return await _httpClient.GetFromJsonAsync<ExchangeRateResponse>(
                $"https://v6.exchangerate-api.com/v6/929975ecf719ec3950979860/latest/{baseCurrency}");
        }
        catch
        {
            return null;
        }
    }
}