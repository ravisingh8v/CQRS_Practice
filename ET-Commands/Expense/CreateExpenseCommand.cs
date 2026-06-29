using ET_DTO;
using MediatR;

namespace ET_Commands.Expense;

public class CreateExpenseCommand(CreateExpenseDto expense) : IRequest<ExpenseDto>
{
    public CreateExpenseDto Expense { get; } = expense;
}
