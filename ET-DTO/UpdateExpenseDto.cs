using System.ComponentModel.DataAnnotations;

namespace ET_DTO;

public class UpdateExpenseDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 200 characters")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public required decimal Amount { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(1000, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 1000 characters")]
    public required string Description { get; set; }
}
