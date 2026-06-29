using ET_DTO;
using MediatR;

namespace ET_Queries.Expense;

public class GetExpenseByIdQuery : IRequest<ExpenseDto?>
{
    public int Id { get; init; }

    public GetExpenseByIdQuery(int id) => Id = id;
}
