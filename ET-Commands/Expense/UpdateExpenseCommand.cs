using ET_DTO;
using MediatR;

namespace ET_Commands.Expense;

public class UpdateExpenseCommand(int Id, UpdateExpenseDto Expense) : IRequest<bool>{
    public int Id { get; } = Id;
    public UpdateExpenseDto Expense { get; } = Expense;
}
