namespace BankAPI.DTos
{
    public class TransferDto
    {
        public int FromCustomerId { get; set; }
        public int ToCustomerId { get; set; }
        public decimal Amount { get; set; }
    }
}
