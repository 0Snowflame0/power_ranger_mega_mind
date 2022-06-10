namespace _22SevenFincancialApp.Models.apiContent
{
  public class RetrieveTransferHistory
  {
    public int RetrieveTransferHistoryId { get; set; }
    public string? sentFrom { get; set; }
    public string? recipient { get; set; }
    public int total { get; set; }
    public DateTime timeOfTransaction { get; set; }
    public bool isTransferSuccesful { get; set; }
    public string? failureResponse { get; set; }
    public int transactionId { get; set; }
  }
}
