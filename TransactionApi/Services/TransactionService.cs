using TransactionApi.Data;
using TransactionApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TransactionApi.Services;

public class TransactionService(
    AppDbContext dbContext,
    ITransactionCacheService cacheService) : ITransactionService
{
   public async Task<Transaction> CreateAsync(Transaction transaction)
{
    transaction.Id = Guid.NewGuid();

    dbContext.Transactions.Add(transaction);
    await dbContext.SaveChangesAsync();

    await cacheService.SetAsync(transaction);

    return transaction;
}

    public async Task<List<Transaction>> GetAllAsync()
    {
        return await dbContext.Transactions.ToListAsync();
    }
    public async Task<Transaction?> GetByIdAsync(Guid id)
{
    var cachedTransaction = await cacheService.GetAsync(id);

    if (cachedTransaction is not null)
    {
        return cachedTransaction;
    }

    var transaction = await dbContext.Transactions.FindAsync(id);

    if (transaction is not null)
    {
        await cacheService.SetAsync(transaction);
    }

    return transaction;
}
}