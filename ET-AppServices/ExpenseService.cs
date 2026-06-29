using ET_Commands.Expense;
using ET_DTO;
using ET_Queries.Expense;
using MediatR;

namespace ET_AppServices;

public class ExpenseService : IExpenseService
{
    private readonly IMediator _mediator;

    public ExpenseService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<List<ExpenseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _mediator.Send(new GetAllExpensesQuery(), cancellationToken);
    }

    public Task<ExpenseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(new GetExpenseByIdQuery(id), cancellationToken);
    }

    public Task<ExpenseDto> CreateAsync(CreateExpenseDto request, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(new CreateExpenseCommand(request), cancellationToken);
    }

    public Task<bool> UpdateAsync(int id, UpdateExpenseDto request, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(new UpdateExpenseCommand(id, request), cancellationToken);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(new DeleteExpenseCommand(id), cancellationToken);
    }
}
