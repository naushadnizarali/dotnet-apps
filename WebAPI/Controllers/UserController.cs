using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : Controller
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<JwtPayload>> All()
    {
        return _userService.All();
    }

    [HttpPost]
    [AllowAnonymous]
    public ActionResult<string> Login(JwtPayload request)
    {
        Console.WriteLine(request);
        var result = _userService.Login(request);

        if (string.IsNullOrEmpty(result))
        {
            return BadRequest("Bad Credentials");
        }

        return Ok(result);
    }

    [HttpGet]
    [Route("GetHello")]
    [AllowAnonymous]
    public string GetHello()
    {
        return "hello";
    }

}
