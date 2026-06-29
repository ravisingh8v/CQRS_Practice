using MediatR;

namespace ET_Commands.Expense;

public class DeleteExpenseCommand(int Id) : IRequest<bool>
{
    public int Id { get; } = Id;
}
