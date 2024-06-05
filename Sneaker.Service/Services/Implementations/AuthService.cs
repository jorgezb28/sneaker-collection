using AutoMapper;
using Sneaker.Domain.Entities;
using Sneaker.Domain.IRepository;
using Sneaker.Service.DTOs.Auth;

namespace Sneaker.Service.Services.Implementations;

public class AuthService: IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AuthService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task Register(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _userRepository.Register(user);
    }

    public async Task<UserDto> FindUserByUserName(string userName)
    {
        var user = await _userRepository.FindUserByUserName(userName);
        return _mapper.Map<UserDto>(user);
    }
}