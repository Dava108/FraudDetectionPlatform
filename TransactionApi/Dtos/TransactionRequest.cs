using System.ComponentModel.DataAnnotations;

namespace TransactionApi.Dtos;

public class TransactionRequest
{
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string Currency { get; set; } = "MXN";

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string CardId { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string MerchantId { get; set; } = string.Empty;
}