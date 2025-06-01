using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
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

        public async Task<bool> DepositAsync(int accountId, decimal amount)
        {
            var dto = new TransactionDto { AccountId = accountId, Amount = amount };
            var response = await _http.PostAsJsonAsync("api/transactions/deposit", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> WithdrawAsync(int accountId, decimal amount)
        {
            var dto = new TransactionDto { AccountId = accountId, Amount = amount };
            var response = await _http.PostAsJsonAsync("api/transactions/withdraw", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> TransferAsync(int fromAccountId, int toAccountId, decimal amount)
        {
            var dto = new TransferDto { FromAccountId = fromAccountId, ToAccountId = toAccountId, Amount = amount };
            var response = await _http.PostAsJsonAsync("api/transactions/transfer", dto);
            return response.IsSuccessStatusCode;
        }

        // 🔄 Ny metod: Hämta transaktioner för ett konto
        public async Task<List<TransactionDto>> GetTransactionsForAccountAsync(int accountId)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<TransactionDto>>($"api/transactions/account/{accountId}");
                return result ?? new List<TransactionDto>();
            }
            catch
            {
                return new List<TransactionDto>();
            }
        }
    }
}
