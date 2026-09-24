using System.ComponentModel.DataAnnotations;
using TransactionApi.Dtos;
using Xunit;

namespace TransactionApi.Tests;

public class TransactionRequestTests
{
    [Fact]
    public void TransactionRequest_ShouldRejectInvalidAmount()
    {
        var request = new TransactionRequest
        {
            Amount = -500,
            Currency = "MXN",
            CardId = "card-test",
            MerchantId = "merchant-test"
        };

        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(
            request,
            context,
            results,
            validateAllProperties: true
        );

        Assert.False(isValid);
        Assert.Contains(
            results,
            r => r.MemberNames.Contains(nameof(TransactionRequest.Amount))
        );
    }

    [Fact]
    public void TransactionRequest_ShouldRejectInvalidCurrency()
    {
        var request = new TransactionRequest
        {
            Amount = 100,
            Currency = "MX",
            CardId = "card-test",
            MerchantId = "merchant-test"
        };

        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(
            request,
            context,
            results,
            validateAllProperties: true
        );

        Assert.False(isValid);
        Assert.Contains(
            results,
            r => r.MemberNames.Contains(nameof(TransactionRequest.Currency))
        );
    }

    [Fact]
    public void TransactionRequest_ShouldRejectEmptyCardId()
    {
        var request = new TransactionRequest
        {
            Amount = 100,
            Currency = "MXN",
            CardId = "",
            MerchantId = "merchant-test"
        };

        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(
            request,
            context,
            results,
            validateAllProperties: true
        );

        Assert.False(isValid);
        Assert.Contains(
            results,
            r => r.MemberNames.Contains(nameof(TransactionRequest.CardId))
        );
    }

    [Fact]
    public void TransactionRequest_ShouldRejectEmptyMerchantId()
    {
        var request = new TransactionRequest
        {
            Amount = 100,
            Currency = "MXN",
            CardId = "card-test",
            MerchantId = ""
        };

        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(
            request,
            context,
            results,
            validateAllProperties: true
        );

        Assert.False(isValid);
        Assert.Contains(
            results,
            r => r.MemberNames.Contains(nameof(TransactionRequest.MerchantId))
        );
    }
}