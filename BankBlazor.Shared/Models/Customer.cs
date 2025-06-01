using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankBlazor.Shared.Models
{
    

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }

    [Column("Givenname")]
    public string Name { get; set; } = string.Empty;

    [Column("Emailaddress")]
    public string Email { get; set; } = string.Empty;

    [Column("Telephonenumber")]
    public string PhoneNumber { get; set; } = string.Empty;

    public ICollection<Account>? Accounts { get; set; }
}

}