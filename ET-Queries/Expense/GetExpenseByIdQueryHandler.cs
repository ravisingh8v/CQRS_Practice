using ET_DTO;
using ET_Infrastructure.Repositories;
using MediatR;

namespace ET_Queries.Expense;

public class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQuery, ExpenseDto?>
{
    private readonly IExpenseRepository _repository;

    public GetExpenseByIdQueryHandler(IExpenseRepository repository)
    {
        _repository = repository;
    }

    public async Task<ExpenseDto?> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
    {
        var expense = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (expense == null)
        {
            return null;
        }

        return new ExpenseDto
        {
            Id = expense.Id,
            Title = expense.Title,
            Amount = expense.Amount,
            Description = expense.Description,
            CreatedAt = expense.CreatedAt,
            UpdatedAt = expense.UpdatedAt
        };
    }
}
