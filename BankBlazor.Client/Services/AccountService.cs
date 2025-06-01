using BankBlazor.Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class AccountService
{
    private readonly HttpClient _http;

    public AccountService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<CustomerWithAccountsDto>> GetCustomersWithAccountsAsync(int page, int pageSize)
    {
        var result = await _http.GetFromJsonAsync<List<CustomerWithAccountsDto>>(
            $"api/account/customers-with-accounts?page={page}&pageSize={pageSize}");
        return result ?? new List<CustomerWithAccountsDto>();
    }
}
