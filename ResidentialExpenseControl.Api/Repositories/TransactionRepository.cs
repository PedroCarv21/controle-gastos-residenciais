using Microsoft.EntityFrameworkCore;
using ResidentialExpenseControl.Api.Data;
using ResidentialExpenseControl.Api.Models;

namespace ResidentialExpenseControl.Api.Repositories;

public class TransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction?> GetByIdAsync(Guid id)
    {
        return await _context.Transactions
            .Include(transaction => transaction.Person)
            .FirstOrDefaultAsync(transaction => transaction.Id == id);
    }

    public async Task<List<Transaction>> GetAllAsync()
    {
        return await _context.Transactions
            .Include(transaction => transaction.Person)
            .AsNoTracking()
            .OrderByDescending(transaction => transaction.Description)
            .ToListAsync();
    }

    public async Task<Transaction> CreateAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);

        await _context.SaveChangesAsync();

        return transaction;
    }

    public async Task<bool> DeleteAsync(Transaction transaction)
    {
        _context.Transactions.Remove(transaction);

        await _context.SaveChangesAsync();

        return true;
    }
}