using System.ComponentModel.DataAnnotations;

namespace Sneaker.Service.DTOs.Auth;

public class UserDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public byte[] PasswordSalt { get; set; }
    [Required]
    public byte[] PasswordHash { get; set; }
}