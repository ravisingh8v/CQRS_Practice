using ET_AppServices;
using ET_Common.Responses;
using ET_DTO;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;
    private readonly ILogger<ExpenseController> _logger;

    public ExpenseController(IExpenseService expenseService, ILogger<ExpenseController> logger)
    {
        _expenseService = expenseService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<List<ExpenseDto>>))]
    public async Task<ActionResult<List<ExpenseDto>>> GetAllExpenses(CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAllExpenses endpoint called");
        var expenses = await _expenseService.GetAllAsync(cancellationToken);
        _logger.LogInformation("Retrieved {Count} expenses", expenses.Count);

        return Ok(expenses);
       
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ExpenseDto>))]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExpenseDto>> GetExpenseById(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetExpenseById endpoint called for id={Id}", id);
        var expense = await _expenseService.GetByIdAsync(id, cancellationToken);
        if (expense == null)
        {
            _logger.LogWarning("Expense not found for id={Id}", id);
            return NotFound();
        }

        return Ok(expense);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExpenseDto>> CreateExpense([FromBody] CreateExpenseDto request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateExpense endpoint called");
        var createdExpense = await _expenseService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetExpenseById), new { id = createdExpense.Id }, createdExpense);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateExpense(int id, [FromBody] UpdateExpenseDto request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdateExpense endpoint called for id={Id}", id);
        var updated = await _expenseService.UpdateAsync(id, request, cancellationToken);
        if (!updated)
        {
            _logger.LogWarning("Expense not found for update id={Id}", id);
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteExpense(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DeleteExpense endpoint called for id={Id}", id);
        var deleted = await _expenseService.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            _logger.LogWarning("Expense not found for delete id={Id}", id);
            return NotFound();
        }

        return NoContent();
    }
}
