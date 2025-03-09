using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ConvertisseurdeDevises.Services;

var builder =  WebApplication.CreateBuilder(args);

// Configuration des services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped(sp => 
    new HttpClient { 
        BaseAddress = new Uri("https://v6.exchangerate-api.com/") // URL de base de l'API
    });
builder.Services.AddScoped<ExchangeRateService>();

var app = builder.Build();

// Configuration du pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host"); // Pointe vers le nouveau _Host.cshtml

app.Run();