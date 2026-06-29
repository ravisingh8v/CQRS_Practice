using ET_Infrastructure.Repositories;
using MediatR;

namespace ET_Commands.Expense;

public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, bool>
{
    private readonly IExpenseRepository _repository;

    public DeleteExpenseCommandHandler(IExpenseRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        var existingExpense = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existingExpense == null)
        {
            return false;
        }

        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
