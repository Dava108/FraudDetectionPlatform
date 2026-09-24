using Microsoft.EntityFrameworkCore;
using TransactionApi.Models;

namespace TransactionApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Transaction> Transactions => Set<Transaction>();
}