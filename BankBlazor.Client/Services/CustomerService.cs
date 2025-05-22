using System.Net.Http.Json;

public class CustomerService
{
    private readonly HttpClient _http;

    public CustomerService(HttpClient http)
    {
        _http = http;
    }

    public async Task<Customer?> GetCustomerAsync(int id)
    {
        return await _http.GetFromJsonAsync<Customer>($"api/customers/{id}");
    }

    public async Task DepositAsync(int accountId, decimal amount)
    {
        await _http.PostAsJsonAsync("api/transactions/deposit", new TransactionDto { AccountId = accountId, Amount = amount });
    }

    public async Task WithdrawAsync(int accountId, decimal amount)
    {
        await _http.PostAsJsonAsync("api/transactions/withdraw", new TransactionDto { AccountId = accountId, Amount = amount });
    }

    public async Task TransferAsync(int fromId, int toId, decimal amount)
    {
        await _http.PostAsJsonAsync("api/transactions/transfer", new TransferDto { FromAccountId = fromId, ToAccountId = toId, Amount = amount });
    }
}
