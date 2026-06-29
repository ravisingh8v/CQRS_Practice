using ET_Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ET_Infrastructure.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly AppDbContext _dbContext;

    public ExpenseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ExpenseModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Expenses
            .Where(e => e.DeletedAt == null)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    // public async Task<List<ExpenseModel>> GetAllAsync(CancellationToken cancellationToken = default)
    // {
    //     return await _dbContext.Expenses
    //         .Where(e => e.DeletedAt == null)
    //         .AsNoTracking()
    //         .ToListAsync(cancellationToken);
    // }

    public async Task<ExpenseModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Expenses
            .FirstOrDefaultAsync(e => e.Id == id && e.DeletedAt == null, cancellationToken);
    }

    public async Task AddAsync(ExpenseModel expense, CancellationToken cancellationToken = default)
    {
        await _dbContext.Expenses.AddAsync(expense, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ExpenseModel expense, CancellationToken cancellationToken = default)
    {
        expense.UpdatedAt = DateTime.UtcNow;
        _dbContext.Expenses.Update(expense);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var expense = await GetByIdAsync(id, cancellationToken);
        if (expense != null)
        {
            expense.DeletedAt = DateTime.UtcNow;
            await UpdateAsync(expense, cancellationToken);
        }
    }
}
