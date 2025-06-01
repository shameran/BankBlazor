using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BankBlazor.Client; 
using BankBlazor.Client.Services; 

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        
        builder.Services.AddScoped<CustomerService>();
        builder.Services.AddScoped<TransactionService>();
        builder.Services.AddScoped<AccountService>();



        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7023/") });

        await builder.Build().RunAsync();
    }
}
