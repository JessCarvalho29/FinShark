using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment;

public class CreateCommentRequestDto
{
    // Adding more complex form of data validation annotations
    // Do not put data validation directly in a model, because it's going to apply globally
    [Required]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters long.")]
    [MaxLength(280, ErrorMessage = "Title must be no more than 280 characters long.")]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MinLength(5, ErrorMessage = "Content must be at least 5 characters long.")]
    [MaxLength(280, ErrorMessage = "Content must be no more than 280 characters long.")]
    public string Content { get; set; } = string.Empty;
    
    // The validations just occurs if we tell dotnet to do it
}