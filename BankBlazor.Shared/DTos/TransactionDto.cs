namespace BankAPI.DTos
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } 
        public DateTime Date { get; set; }
    }
}
