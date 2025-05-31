using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BankBlazor.Client; // Kontrollera att detta �r r�tt namespace f�r din klient
using BankBlazor.Client.Services; // F�r TransactionService och CustomerService

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        // Registrera dina services s� att de kan injectas i komponenterna
        builder.Services.AddScoped<CustomerService>();
        builder.Services.AddScoped<TransactionService>();

        // Viktigt: BaseAddress ska peka p� din API (backend)
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7023/") });

        await builder.Build().RunAsync();
    }
}
