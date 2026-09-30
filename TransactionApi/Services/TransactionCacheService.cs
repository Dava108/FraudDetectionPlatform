using System.Text.Json;
using TransactionApi.Models;

namespace TransactionApi.Services;

public class TransactionCacheService(IRedisService redis) : ITransactionCacheService
{
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(10);

    public async Task SetAsync(Transaction transaction)
    {
        var json = JsonSerializer.Serialize(transaction);

        await redis.SetAsync(
            $"transaction:{transaction.Id}",
            json,
            CacheExpiration
        );
    }

    public async Task<Transaction?> GetAsync(Guid id)
    {
        var json = await redis.GetAsync($"transaction:{id}");

        if (json is null)
        {
            return null;
        }

        return JsonSerializer.Deserialize<Transaction>(json);
    }

    public async Task DeleteAsync(Guid id)
    {
        await redis.DeleteAsync($"transaction:{id}");
    }
}