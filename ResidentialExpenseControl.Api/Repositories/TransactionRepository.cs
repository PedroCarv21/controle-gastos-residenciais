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
}