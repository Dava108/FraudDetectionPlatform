using TransactionApi.Data;
using TransactionApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TransactionApi.Services;

public class TransactionService(AppDbContext dbContext) : ITransactionService
{
    public async Task<Transaction> CreateAsync(Transaction transaction)
    {
        transaction.Id = Guid.NewGuid();

        dbContext.Transactions.Add(transaction);
        await dbContext.SaveChangesAsync();

        return transaction;
    }

    public async Task<List<Transaction>> GetAllAsync()
    {
        return await dbContext.Transactions.ToListAsync();
    }
    public async Task<Transaction?> GetByIdAsync(Guid id)
    {
    return await dbContext.Transactions.FindAsync(id);
    }
}