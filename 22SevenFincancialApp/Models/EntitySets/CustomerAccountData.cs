namespace _22SevenFincancialApp.Models.EntitySets
{
  public enum AccountType { savings, check }
  public class CustomerAccountData
  {
    public int Id { get; set; }
    public string? AccountName { get; set; }
    public decimal? Balance { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? CustomerId { get; set; }
    public AccountType AccountType { get; set; }
  }
}
