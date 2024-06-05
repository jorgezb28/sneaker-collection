namespace Sneaker.Domain.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}