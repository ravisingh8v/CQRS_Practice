using ET_DTO;

namespace ET_AppServices;

public interface IExpenseService
{
    Task<List<ExpenseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ExpenseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ExpenseDto> CreateAsync(CreateExpenseDto request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, UpdateExpenseDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
