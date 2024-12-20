using Microsoft.AspNetCore.Mvc; // Para ApiController, ControllerBase, IActionResult
using System.Threading.Tasks;
using YourNamespace.Dtos; // Replace 'YourNamespace.Dtos' with the actual namespace where UserDto is defined

 // Para métodos assíncronos
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        var user = await _userService.Register(userDto); 
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var token = await _userService.Login(loginDto);
        return Ok(new { Token = token });
    }
}
