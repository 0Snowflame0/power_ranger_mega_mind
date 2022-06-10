using _22SevenFincancialApp.Models.EntitySets;

namespace _22SevenFincancialApp.Models.apiContent
{
  
  public class Accounts
  {
    public int Id { get; set; }
    public string? accountName { get; set; }
    public decimal initialDeposit { get; set; } 
    public decimal balance { get; set; }
    public AccountType AccountType { get; set; } 
  }
}
