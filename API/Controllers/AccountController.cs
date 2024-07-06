using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Dtos;
using API.Enteties;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService, ILogger<AccountController> logger) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) return BadRequest("Username is taken!");
        using var hmac = new HMACSHA512();
        var user = new AppUser
        {
            UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key,
            Gender = registerDto.Gender,
            City = registerDto.City,
            Country = registerDto.Country,
            KnownAs = registerDto.Knownas
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();
        var token = tokenService.CreateToken(user);
        var userDto = new UserDTO
        {
            Username = user.UserName,
            Token = token
        };
        return userDto;
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDto loginDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginDto.Username.ToLower());
        if (user == null) return Unauthorized("User does not exist!");
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Username.ToLower()));
        for (int i = 0; i < computedHash.Length; i++)
        {
            logger.LogInformation(computedHash[i].ToString());
            if (computedHash[i] != user.PasswordHash[i]) return BadRequest("Password does not match");
        }
        return new UserDTO
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower());
    }
}
