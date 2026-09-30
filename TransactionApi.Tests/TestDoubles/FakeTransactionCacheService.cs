using TransactionApi.Models;
using TransactionApi.Services;

namespace TransactionApi.Tests.TestDoubles;

public class FakeTransactionCacheService : ITransactionCacheService
{
    private readonly Dictionary<Guid, Transaction> _cache = new();

    public Task SetAsync(Transaction transaction)
    {
        _cache[transaction.Id] = transaction;

        return Task.CompletedTask;
    }

    public Task<Transaction?> GetAsync(Guid id)
    {
        _cache.TryGetValue(id, out var transaction);

        return Task.FromResult(transaction);
    }

    public Task DeleteAsync(Guid id)
    {
        _cache.Remove(id);

        return Task.CompletedTask;
    }
}