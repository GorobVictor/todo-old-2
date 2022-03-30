using core.DTO.UserDTO;
using core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace todo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private IUserService UserSvc { get; set; }
    
    public AccountController(IUserService userSvc)
    {
        this.UserSvc = userSvc;
    }
    
    [AllowAnonymous]
    [HttpPost]
    [Route("sing-in")]
    public async Task<IActionResult> SignIn([FromBody]LoginDto user)
    {
        return Ok(await this.UserSvc.GetIdentityAsync(user));
    }
    
    [AllowAnonymous]
    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SignUp(UserSignUp user)
    {
        return Ok(await this.UserSvc.SignUp(user));
    }
}