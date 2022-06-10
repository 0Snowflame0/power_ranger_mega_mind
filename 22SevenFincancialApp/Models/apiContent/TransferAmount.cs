namespace _22SevenFincancialApp.Models.apiContent
{
  public class TransferAmount
  {
    /// <summary>
    /// Customer Id to know who is initiating the transfer
    /// </summary>
    public int CustomerId { get; set; }
    /// <summary>
    /// customer name to track who the transfer is coming from
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// the total amount to be transferred 
    /// </summary>
    public decimal? AmountToTransfer { get; set; }
    /// <summary>
    /// transaction from account -- account which the money will be coming from
    /// </summary>
    public Accounts? AccountFrom { get; set; }
    /// <summary>
    /// transaction to account --  account which the money will go to
    /// </summary>
    public Accounts? AccountTo { get; set; }
  }
}
