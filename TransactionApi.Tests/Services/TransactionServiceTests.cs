using Microsoft.EntityFrameworkCore;
using TransactionApi.Data;
using TransactionApi.Models;
using TransactionApi.Services;
using TransactionApi.Tests.TestDoubles;

using Xunit;

namespace TransactionApi.Tests;

public class TransactionServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldSaveTransaction()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var dbContext = new AppDbContext(options);

        var cacheService = new FakeTransactionCacheService();
        var kafkaProducer = new FakeKafkaProducerService();

        var service = new TransactionService(
            dbContext,
            cacheService,
            kafkaProducer);

        var transaction = new Transaction
        {
            Amount = 1500,
            Currency = "MXN",
            CardId = "card-test",
            MerchantId = "merchant-test"
        };

        var result = await service.CreateAsync(transaction);

        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(1500, result.Amount);
        Assert.Equal("MXN", result.Currency);
        Assert.Single(dbContext.Transactions);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTransaction()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var dbContext = new AppDbContext(options);

        var cacheService = new FakeTransactionCacheService();
        var kafkaProducer = new FakeKafkaProducerService();

        var service = new TransactionService(
            dbContext,
            cacheService,
            kafkaProducer);

        var transaction = new Transaction
        {
            Amount = 2500,
            Currency = "MXN",
            CardId = "card-test-2",
            MerchantId = "merchant-test-2"
        };

        var created = await service.CreateAsync(transaction);

        var result = await service.GetByIdAsync(created.Id);

        Assert.NotNull(result);
        Assert.Equal(created.Id, result.Id);
        Assert.Equal(2500, result.Amount);
        Assert.Equal("card-test-2", result.CardId);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenTransactionDoesNotExist()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var dbContext = new AppDbContext(options);

        var cacheService = new FakeTransactionCacheService();
        var kafkaProducer = new FakeKafkaProducerService();

        var service = new TransactionService(
            dbContext,
            cacheService,
            kafkaProducer);

        var result = await service.GetByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }
    [Fact]
    public async Task GetByIdAsync_ShouldReturnCachedTransaction()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var dbContext = new AppDbContext(options);

        var cacheService = new FakeTransactionCacheService();
        var kafkaProducer = new FakeKafkaProducerService();

        var service = new TransactionService(
            dbContext,
            cacheService,
            kafkaProducer);

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Amount = 3000,
            Currency = "MXN",
            CardId = "cached-card",
            MerchantId = "cached-merchant"
        };

        await cacheService.SetAsync(transaction);

        var result = await service.GetByIdAsync(transaction.Id);

        Assert.NotNull(result);
        Assert.Equal(transaction.Id, result.Id);
        Assert.Equal(3000, result.Amount);
        Assert.Equal("cached-card", result.CardId);
    }




    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTransactions()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var dbContext = new AppDbContext(options);

        var cacheService = new FakeTransactionCacheService();
        var kafkaProducer = new FakeKafkaProducerService();

        var service = new TransactionService(
            dbContext,
            cacheService,
            kafkaProducer);

        await service.CreateAsync(new Transaction
        {
            Amount = 1000,
            Currency = "MXN",
            CardId = "card-1",
            MerchantId = "merchant-1"
        });

        await service.CreateAsync(new Transaction
        {
            Amount = 2000,
            Currency = "MXN",
            CardId = "card-2",
            MerchantId = "merchant-2"
        });

        var result = await service.GetAllAsync();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, t => t.Amount == 1000);
        Assert.Contains(result, t => t.Amount == 2000);
    }
}