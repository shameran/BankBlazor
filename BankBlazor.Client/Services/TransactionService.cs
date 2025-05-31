using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BankBlazor.Shared.Dtos;

namespace BankBlazor.Client.Services
{
    public class TransactionService
    {
        private readonly HttpClient _http;

        public TransactionService(HttpClient http)
        {
            _http = http;
        }

        public async Task DepositAsync(int accountId, decimal amount)
        {
            var dto = new TransactionDto { AccountId = accountId, Amount = amount };
            var response = await _http.PostAsJsonAsync("api/transactions/deposit", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task WithdrawAsync(int accountId, decimal amount)
        {
            var dto = new TransactionDto { AccountId = accountId, Amount = amount };
            var response = await _http.PostAsJsonAsync("api/transactions/withdraw", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task TransferAsync(int fromAccountId, int toAccountId, decimal amount)
        {
            var dto = new TransferDto { FromAccountId = fromAccountId, ToAccountId = toAccountId, Amount = amount };
            var response = await _http.PostAsJsonAsync("api/transactions/transfer", dto);
            response.EnsureSuccessStatusCode();
        }
    }
}
