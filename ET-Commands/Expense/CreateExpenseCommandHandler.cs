using ET_DTO;
using ET_Infrastructure.Models;
using ET_Infrastructure.Repositories;
using MediatR;

namespace ET_Commands.Expense;

public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, ExpenseDto>
{
    private readonly IExpenseRepository _repository;

    public CreateExpenseCommandHandler(IExpenseRepository repository)
    {
        _repository = repository;
    }

    public async Task<ExpenseDto> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = new ExpenseModel
        {
            Title = request.Expense.Title,
            Amount = request.Expense.Amount,
            Description = request.Expense.Description
        };

        await _repository.AddAsync(expense, cancellationToken);

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
