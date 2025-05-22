namespace BankAPI.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }

        
        public Customer Customer { get; set; }
    }
}