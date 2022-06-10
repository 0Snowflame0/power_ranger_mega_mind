namespace _22SevenFincancialApp.Models.apiContent
{
  public class RetrieveBalance
  {
    /// <summary>
    /// customer id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// name of customer viewing balance
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// accounts with funds
    /// </summary>
    public Accounts[]? accounts { get; set; }
  }
}
