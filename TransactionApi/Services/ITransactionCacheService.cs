using TransactionApi.Models;

namespace TransactionApi.Services;

public interface ITransactionCacheService
{
    Task SetAsync(Transaction transaction);
    Task<Transaction?> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
}