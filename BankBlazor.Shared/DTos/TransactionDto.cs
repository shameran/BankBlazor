namespace BankBlazor.Shared.Dtos
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal BalanceAfter { get; set; } 
    }
}
