namespace _22SevenFincancialApp.Models.apiContent
{
  public class CreateAccount
  {
    /// <summary>
    /// Unique id that will link user to accounts and transactions.
    /// </summary>
    public int customerId { get; set; }
    /// <summary>
    /// user name/customer name.
    /// </summary>
    public string? name { get; set; }
    /// <summary>
    /// Array of accounts. can be 1 or many.
    /// </summary>
    public Accounts[]? AccountInfo { get; set; }

  }
}
