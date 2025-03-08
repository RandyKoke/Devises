using System.Net.Http.Json;
using ConvertisseurdeDevises.Models;
using Microsoft.Extensions.Configuration;

namespace ConvertisseurdeDevises.Services;

public class ExchangeRateService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public ExchangeRateService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["ExchangeRateApiKey"]; // Clé lue automatiquement
    }

    public async Task<ExchangeRateResponse> GetLatestRatesAsync(string baseCurrency)
    {
        var response = await _httpClient.GetAsync($"v6/{_apiKey}/latest/{baseCurrency}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ExchangeRateResponse>();
    }
}

