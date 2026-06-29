using ET_DTO;
using ET_Infrastructure.Repositories;
using MediatR;

namespace ET_Queries.Expense;

public class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQuery, List<ExpenseDto>>
{
    private readonly IExpenseRepository _repository;

    public GetAllExpensesQueryHandler(IExpenseRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ExpenseDto>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
    {
        var expenses = await _repository.GetAllAsync(cancellationToken);
        return expenses.Select(e => new ExpenseDto
        {
            Id = e.Id,
            Title = e.Title,
            Amount = e.Amount,
            Description = e.Description,
            CreatedAt = e.CreatedAt,
            UpdatedAt = e.UpdatedAt
        }).ToList();
    }
}
