using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankBlazor.Shared.Dtos;
using BankBlazor.Shared.Models;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly BankDbContext _context;

        public TransactionsController(BankDbContext context)
        {
            _context = context;
        }

        // GET: api/transactions/{id} - Hämtar en enskild transaktion
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions
                .Where(t => t.TransactionId == id)
                .Select(t => new TransactionDto
                {
                    TransactionId = t.TransactionId,
                    AccountId = t.AccountId,
                    Amount = t.Amount,
                    Type = t.Type,
                    Date = t.Date
                })
                .FirstOrDefaultAsync();

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        // GET: api/transactions/account/{accountId} - Hämtar alla transaktioner för ett konto
        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<List<TransactionDto>>> GetTransactionsByAccount(int accountId)
        {
            var transactions = await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .Select(t => new TransactionDto
                {
                    TransactionId = t.TransactionId,
                    AccountId = t.AccountId,
                    Amount = t.Amount,
                    Type = t.Type,
                    Date = t.Date
                })
                .ToListAsync();

            return Ok(transactions);
        }

        // Här kan du lägga till fler metoder för insättning, uttag, överföring etc.
    }
}
