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

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionDto request)
        {
            if (request.Amount <= 0)
                return BadRequest("Beloppet måste vara större än 0.");

            var account = await _context.Accounts.FindAsync(request.AccountId);
            if (account == null)
                return NotFound("Konto hittades inte.");

            account.Balance += request.Amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = account.AccountId,
                Amount = request.Amount,
                Type = "Credit",             // Credit för insättning
                Operation = "Deposit",
                Date = DateTime.UtcNow,
                Balance = account.Balance
            });

            await _context.SaveChangesAsync();

            return Ok("Insättning genomförd.");
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionDto request)
        {
            if (request.Amount <= 0)
                return BadRequest("Beloppet måste vara större än 0.");

            var account = await _context.Accounts.FindAsync(request.AccountId);
            if (account == null)
                return NotFound("Konto hittades inte.");

            if (account.Balance < request.Amount)
                return BadRequest("Otillräckligt saldo.");

            account.Balance -= request.Amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = account.AccountId,
                Amount = -request.Amount,
                Type = "Debit",              // Debit för uttag
                Operation = "Withdraw",
                Date = DateTime.UtcNow,
                Balance = account.Balance
            });

            await _context.SaveChangesAsync();

            return Ok("Uttag genomfört.");
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDto request)
        {
            if (request.Amount <= 0)
                return BadRequest("Beloppet måste vara större än 0.");

            var fromAccount = await _context.Accounts.FindAsync(request.FromAccountId);
            var toAccount = await _context.Accounts.FindAsync(request.ToAccountId);

            if (fromAccount == null || toAccount == null)
                return NotFound("Ett eller båda konton kunde inte hittas.");

            if (fromAccount.Balance < request.Amount)
                return BadRequest("Otillräckligt saldo.");

            fromAccount.Balance -= request.Amount;
            toAccount.Balance += request.Amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = fromAccount.AccountId,
                Amount = -request.Amount,
                Type = "Debit",              // Debit för konto som skickar pengar
                Operation = "Transfer Out",
                Date = DateTime.UtcNow,
                Balance = fromAccount.Balance
            });

            _context.Transactions.Add(new Transaction
            {
                AccountId = toAccount.AccountId,
                Amount = request.Amount,
                Type = "Credit",             // Credit för konto som tar emot pengar
                Operation = "Transfer In",
                Date = DateTime.UtcNow,
                Balance = toAccount.Balance
            });

            await _context.SaveChangesAsync();

            return Ok("Överföring genomförd.");
        }
    }
}

