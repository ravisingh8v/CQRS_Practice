using ET_Infrastructure.Models;

namespace ET_Infrastructure.Repositories;

public interface IExpenseRepository
{
    Task<List<ExpenseModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ExpenseModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(ExpenseModel expense, CancellationToken cancellationToken = default);
    Task UpdateAsync(ExpenseModel expense, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
