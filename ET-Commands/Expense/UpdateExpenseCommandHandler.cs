using ET_Infrastructure.Repositories;
using MediatR;

namespace ET_Commands.Expense;

public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand, bool>
{
    private readonly IExpenseRepository _repository;

    public UpdateExpenseCommandHandler(IExpenseRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        var existingExpense = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existingExpense == null)
        {
            return false;
        }

        existingExpense.Title = request.Expense.Title;
        existingExpense.Amount = request.Expense.Amount;
        existingExpense.Description = request.Expense.Description;
        existingExpense.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existingExpense, cancellationToken);
        return true;
    }
}
