using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BankBlazor.Shared.Dtos;
using BankBlazor.Shared.Models;

public class CustomerService
{
    private readonly HttpClient _http;

    public CustomerService(HttpClient http)
    {
        _http = http;
    }

    // Hämta en kund via ID
    public async Task<Customer?> GetCustomerAsync(int id)
    {
        return await _http.GetFromJsonAsync<Customer>($"api/customers/{id}");
    }

    // Hämta alla kunder
    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        var customers = await _http.GetFromJsonAsync<List<Customer>>("api/customers");
        return customers ?? new List<Customer>();
    }

    // Hämta konton för alla kunder (om API:et har en sådan endpoint)
    public async Task<List<Account>> GetAccountsAsync()
    {
        var accounts = await _http.GetFromJsonAsync<List<Account>>("api/customers/accounts");
        return accounts ?? new List<Account>();
    }

    // Genomför insättning på ett konto
    public async Task DepositAsync(int accountId, decimal amount)
    {
        var dto = new TransactionDto { AccountId = accountId, Amount = amount };
        await _http.PostAsJsonAsync("api/transactions/deposit", dto);
    }

    // Genomför uttag från ett konto
    public async Task WithdrawAsync(int accountId, decimal amount)
    {
        var dto = new TransactionDto { AccountId = accountId, Amount = amount };
        await _http.PostAsJsonAsync("api/transactions/withdraw", dto);
    }

    // Genomför överföring mellan två konton
    public async Task TransferAsync(int fromAccountId, int toAccountId, decimal amount)
    {
        var dto = new TransferDto { FromAccountId = fromAccountId, ToAccountId = toAccountId, Amount = amount };
        await _http.PostAsJsonAsync("api/transactions/transfer", dto);
    }
}
