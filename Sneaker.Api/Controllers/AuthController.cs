using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sneaker.Service.DTOs.Auth;
using Sneaker.Service.Services;

namespace Sneaker.Api.Controllers;

public class AuthController :Controller
{
    private readonly IAuthService _authService;
    private readonly AppSetting _appSetting;

    public AuthController(IAuthService authService, IOptions<AppSetting> appSettings)
    {
        _authService = authService;
        _appSetting = appSettings.Value;
    }
    
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterUserDto userDto)
    {
        if (string.IsNullOrWhiteSpace(userDto.UserName))
        {
            return BadRequest("UserName invalid.");
        }
        
        if (string.IsNullOrWhiteSpace(userDto.Password))
        {
            return BadRequest("Password invalid.");
        }
        
        var userToRegisterDto = new UserDto()
        {
            UserName = userDto.UserName
        };

        using (HMACSHA512? hmac = new HMACSHA512())
        {
            userToRegisterDto.PasswordSalt = hmac.Key;
            userToRegisterDto.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userDto.Password));
        }

        _authService.Register(userToRegisterDto);
        return Ok(userToRegisterDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var existingUser = await _authService.FindUserByUserName(loginDto.UserName);
            if (existingUser== null)
            {
                return BadRequest("UserName doesn't exists.");
            }

            var passwordMatch = CheckPassword(loginDto.Password, existingUser);
            if (!passwordMatch)
            {
                return BadRequest("UserName or Password is invalid");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetting.ApiSecret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", existingUser.UserName) }),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);
            return Ok(new { token = encryptedToken, username = existingUser.UserName });
        }
        catch (Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
        }
       
    }

    private bool CheckPassword(string loginPassword, UserDto userDto)
    {
        bool result;
        using (HMACSHA512? hmac = new HMACSHA512( userDto.PasswordSalt))
        {
            var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginPassword));
            result = compute.SequenceEqual(userDto.PasswordHash);
        }

        return result;
    }
}