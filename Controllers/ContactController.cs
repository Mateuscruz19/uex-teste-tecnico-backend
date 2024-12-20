using Microsoft.AspNetCore.Authorization;  // Adicionar o namespace para o atributo Authorize
using Microsoft.AspNetCore.Mvc; // Para ApiController, ControllerBase, IActionResult
using System.Threading.Tasks;
using System.Security.Claims; // Add this line to import ClaimTypes
using YourNamespace.Dtos; // Ensure this is the correct namespace for ContactDto

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly ContactService _contactService;

    public ContactController(ContactService contactService)
    {
        _contactService = contactService;
    }

    // Protegendo a rota com o atributo Authorize
    [HttpGet]
    [Authorize]  // Só usuários autenticados podem acessar essa rota
    public async Task<IActionResult> GetAllContacts()
    {
       var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Use ClaimTypes.NameIdentifier

if (userIdClaim == null)
{
    return Unauthorized("User identity is not available.");
}

        // Garantir que o userId é um número válido
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return BadRequest("Invalid user ID.");
        }

        var contacts = await _contactService.GetAllContactsForUser(userId); // Busca contatos para o usuário logado
        return Ok(contacts);
    }

    // Protegendo a rota com o atributo Authorize
    [HttpPost]
    [Authorize]  // Só usuários autenticados podem adicionar um contato
    public async Task<IActionResult> AddContact([FromBody] ContactDto contactDto)
    {
        var userIdClaim = User.FindFirst("nameid")?.Value;  // Mudança aqui para pegar 'nameid'

        if (userIdClaim == null)
        {
            return Unauthorized("User identity is not available.");
        }

        // Garantir que o userId é um número válido
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return BadRequest("Invalid user ID.");
        }

        var contact = await _contactService.AddContactForUser(contactDto, userId);  // Cria um contato para o usuário
        return CreatedAtAction(nameof(GetAllContacts), new { id = contact.Id }, contact);
    }
}
