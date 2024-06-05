using System.ComponentModel.DataAnnotations;

namespace Sneaker.Service.DTOs.Auth;

public class RegisterUserDto
{
    [Required(ErrorMessage = "UserName is required")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }
    
    // Here could be more register user information.
    
}