namespace _22SevenFincancialApp.Models.EntitySets
{
  public class HistoryData
  {
    public string? Id { get; set; }
    public string? customerName { get; set; }
    public string? AccountName { get; set; }
    public DateTime? TransactionDate { get; set; }
    public decimal? TransactionAmount { get; set; }
    public string? DestinationAccount { get; set; }
    public int transactionId { get; set; }
  }
}
