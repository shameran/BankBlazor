namespace BankBlazor.Shared.Dtos
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string AccountType { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }

    public class CustomerWithAccountsDto
    {
        public int CustomerId { get; set; }
        public string Givenname { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public List<AccountDto> Accounts { get; set; } = new();

        
        public string FullName => $"{Givenname} {Surname}";
    }
}
