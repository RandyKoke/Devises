using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ConvertisseurdeDevises;
using ConvertisseurdeDevises.Services;
using Microsoft.Extensions.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Ajout du client HttpClient pour appeler l'API ExchangeRate-API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://v6.exchangerate-api.com/v6/") });

// Ajout du service personnalisé pour gérer les taux de change
builder.Services.AddScoped<ExchangeRateService>();

// Configuration pour lire appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

await builder.Build().RunAsync();

