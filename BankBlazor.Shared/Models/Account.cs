namespace BankBlazor.Shared.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountType { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
