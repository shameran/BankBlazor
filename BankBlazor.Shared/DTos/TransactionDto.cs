namespace BankBlazor.Shared.Dtos
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }  
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }
}
