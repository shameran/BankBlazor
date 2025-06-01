namespace BankBlazor.Shared.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public string AccountType { get; set; } = string.Empty;

        public Customer? Customer { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
    }
}
