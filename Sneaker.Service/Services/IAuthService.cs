using Sneaker.Service.DTOs.Auth;

namespace Sneaker.Service.Services;

public interface IAuthService
{
    Task Register(UserDto userDto);
    Task<UserDto> FindUserByUserName(string userName);
}