using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Register;

public class RegisterRequestDto
{
    [Required]
    public string? Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    
    [Required]
    // [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    // The most part of the validations will be created by the user manage / createAsync()
}