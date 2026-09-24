using TransactionApi.Models;

namespace TransactionApi.Services;

public interface ITransactionService
{
    Task<Transaction> CreateAsync(Transaction transaction);
    Task<List<Transaction>> GetAllAsync();
    Task<Transaction?> GetByIdAsync(Guid id);
}