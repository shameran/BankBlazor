using BankBlazor.Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

public class AccountService
{
    private readonly HttpClient _http;

    public AccountService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<CustomerWithAccountsDto>> GetCustomersWithAccountsAsync(int page, int pageSize)
    {
        try
        {
            var response = await _http.GetFromJsonAsync<List<CustomerWithAccountsDto>>(
                $"api/account/customers-with-accounts?page={page}&pageSize={pageSize}");

            if (response == null)
            {
                Console.WriteLine("API svarade med null – tom lista returneras.");
                return new List<CustomerWithAccountsDto>();
            }

            Console.WriteLine($"Hämtade {response.Count} kunder från API.");
            return response;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Fel vid hämtning från API: {ex.Message}");
            return new List<CustomerWithAccountsDto>();
        }
    }
}
