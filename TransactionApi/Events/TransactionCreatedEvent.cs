namespace TransactionApi.Events;

public class TransactionCreatedEvent
{
    public Guid TransactionId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string CardId { get; set; } = string.Empty;
    public string MerchantId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}