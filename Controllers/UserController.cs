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
        try
        {
            var user = await _userService.Register(userDto);
            return Ok(user); // Retorna o usuário registrado com sucesso
        }
        catch (ArgumentException ex)
        {
            // Retorna 400 se o email já estiver em uso
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            // Retorna 500 em caso de erro interno não esperado
            return StatusCode(500, new { message = "Ocorreu um erro ao processar a solicitação.", details = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        try
        {
            var token = await _userService.Login(loginDto);
            return Ok(new { Token = token }); // Retorna o token JWT gerado
        }
        catch (UnauthorizedAccessException ex)
        {
            // Retorna 401 se as credenciais forem inválidas
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            // Retorna 500 em caso de erro interno não esperado
            return StatusCode(500, new { message = "Ocorreu um erro ao processar a solicitação.", details = ex.Message });
        }
    }
}
