using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Register;

public class LoginRequestDto
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
}