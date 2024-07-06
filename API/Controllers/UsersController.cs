using API.Data;
using API.Dtos;
using API.Enteties;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


[Authorize]
public class UsersController(IUserRepository userRepository, IMapper _mapper) : BaseApiController
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        // var users = await userRepository.GetUsersAsync();
        // var memberDto = _mapper.Map<List<MemberDto>>(users);
        // return Ok(memberDto);
        return Ok(await userRepository.GetMembersAsync());
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MemberDto>> GetUserById(int id)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        if (user != null)
        {
            return _mapper.Map<MemberDto>(user);
        }
        return NotFound("User with provided ID does not exist");
    }
    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUserByUserName(string username)
    {
        var user = await userRepository.GetMemberAsync(username);
        if (user != null)
        {
            return user;
        }
        return NotFound("User does not exist");
    }
}
