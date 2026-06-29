using System.ComponentModel.DataAnnotations;

namespace ET_Infrastructure.Models;

public class ExpenseModel: BaseModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 200 characters")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    public required decimal Amount { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(1000, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 1000 characters")]
    public required string Description { get; set; }
}