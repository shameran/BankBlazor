using BankBlazor.Shared.Dtos;
using BankBlazor.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly BankDbContext _context;

        public AccountController(BankDbContext context)
        {
            _context = context;
        }

        [HttpGet("customers-with-accounts")]
        public async Task<ActionResult<List<CustomerWithAccountsDto>>> GetCustomersWithAccounts(int page = 1, int pageSize = 20)
        {
            if (_context.Customers == null)
                return StatusCode(500, "Customers-tabellen är inte tillgänglig.");

            var customers = await _context.Customers
                .Include(c => c.Accounts)
                .OrderBy(c => c.CustomerId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = customers.Select(c => new CustomerWithAccountsDto
            {
                CustomerId = c.CustomerId,
                Givenname = c.Name,  
                Surname = "",        
                Accounts = c.Accounts?.Select(a => new AccountDto
                {
                    AccountId = a.AccountId,
                    AccountType = a.AccountType,
                    Balance = a.Balance
                }).ToList() ?? new List<AccountDto>()
            }).ToList();

            return Ok(result);
        }
    }
}
