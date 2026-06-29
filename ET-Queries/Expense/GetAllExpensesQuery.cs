using ET_DTO;
using MediatR;

namespace ET_Queries.Expense;

public class GetAllExpensesQuery : IRequest<List<ExpenseDto>>
{
}
