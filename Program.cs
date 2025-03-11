using Microsoft.AspNetCore.Components.Web; 
using Microsoft.AspNetCore.Components.WebAssembly.Hosting; 
using Blazored.LocalStorage; 
using ConvertisseurdeDevises; 
using ConvertisseurdeDevises.Services; 
using Microsoft.Extensions.Configuration; 
 
var builder = WebAssemblyHostBuilder.CreateDefault(args); 
builder.RootComponents.Add<App>("#app"); 
builder.RootComponents.Add<HeadOutlet>("head::after"); 
 
// Configuration automatique via wwwroot/appsettings.json (Point super important !!!)
builder.Services.AddScoped(sp => new HttpClient {  
    BaseAddress = new Uri("https://v6.exchangerate-api.com/v6/")  
}); 
 
// Ajouter après les autres services
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<FavoriteService>();


// Injection du service avec accès à la configuration 
builder.Services.AddScoped<ExchangeRateService>(); 
 
await builder.Build().RunAsync();




