using API.Data;
using API.Enteties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController(DataContext context) : BaseApiController
{
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetAuth() => "secret test";
    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        var user = context.Users.Find(-1);
        if (user == null) return NotFound();
        return user;
    }
    [HttpGet("server-error")]
    public ActionResult<AppUser> GetServerError()
    {
        var user = context.Users.Find(-1) ?? throw new Exception("Bad things has happend");
        return user;


    }
    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This was not a good request!");
    }
}
