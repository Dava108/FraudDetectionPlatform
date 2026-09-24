using System.ComponentModel.DataAnnotations;
using TransactionApi.Dtos;
using Xunit;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TransactionApi.Tests;
public class TransactionsApiTests
{
    [Fact]
public async Task PostTransaction_ShouldReturnCreated()
{
    await using var factory = new WebApplicationFactory<Program>();
    using var client = factory.CreateClient();

    var request = new
    {
        amount = 1500,
        currency = "MXN",
        cardId = "card-http-test",
        merchantId = "merchant-http-test"
    };

    var response = await client.PostAsJsonAsync("/transactions", request);

    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
}
[Fact]
public async Task PostTransaction_ShouldReturnBadRequest_WhenRequestIsInvalid()
{
    await using var factory = new WebApplicationFactory<Program>();
    using var client = factory.CreateClient();

    var request = new
    {
        amount = -500,
        currency = "",
        cardId = "",
        merchantId = ""
    };

    var response = await client.PostAsJsonAsync("/transactions", request);

    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
}
[Fact]
public async Task GetTransaction_ShouldReturnNotFound_WhenTransactionDoesNotExist()
{
    await using var factory = new WebApplicationFactory<Program>();
    using var client = factory.CreateClient();

    var id = Guid.NewGuid();

    var response = await client.GetAsync($"/transactions/{id}");

    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}
}