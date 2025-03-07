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
        _apiKey = configuration["ExchangeRateApiKey"]; // Clé API stockée dans la configuration
    }

    public async Task<ExchangeRateResponse> GetLatestRatesAsync(string baseCurrency)
    {
        try
        {
            var response = await _httpClient.GetAsync($"v6/{_apiKey}/latest/{baseCurrency}");
            response.EnsureSuccessStatusCode(); // Lance une exception si la réponse n'est pas OK
            var result = await response.Content.ReadFromJsonAsync<ExchangeRateResponse>();
            return result;
        }
        catch (HttpRequestException ex)
        {
            // Gérez l'erreur ici (journalisation, affichage d'un message d'erreur)
            Console.WriteLine($"Erreur lors de l'appel API : {ex.Message}");
            return null; // Ou lancez une exception personnalisée
        }
    }
}