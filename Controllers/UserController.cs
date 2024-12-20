using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Dtos;

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
            return Ok(user);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocorreu um erro ao processar a solicitação.", details = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        try
        {
            var token = await _userService.Login(loginDto);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocorreu um erro ao processar a solicitação.", details = ex.Message });
        }
    }

    // Rota protegida para deletar a conta e contatos do usuário
    [HttpDelete("delete-account")]
    [Authorize] // Esta rota exige que o usuário esteja autenticado
    public async Task<IActionResult> DeleteAccount()
    {
        try
        {
            // Obtém o ID do usuário a partir do token JWT
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "ID do usuário não encontrado no token." });
            }

            var userId = int.Parse(userIdClaim);

            // Chama o UserService para excluir o usuário e seus contatos
            var success = await _userService.DeleteUserAndContacts(userId);

            if (!success)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }

            return Ok(new { message = "Conta e contatos excluídos com sucesso." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocorreu um erro ao processar a solicitação.", details = ex.Message });
        }
    }
}
