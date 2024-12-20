using Microsoft.AspNetCore.Authorization;  // Adicionar o namespace para o atributo Authorize
using Microsoft.AspNetCore.Mvc; // Para ApiController, ControllerBase, IActionResult
using System.Threading.Tasks;
using System.Security.Claims; // Add this line to import ClaimTypes
using YourNamespace.Dtos; // Ensure this is the correct namespace for ContactDto
using Microsoft.Extensions.Logging; 


[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly ContactService _contactService;
    private readonly ILogger<ContactController> _logger;  // Injeção de dependência de ILogger

    public ContactController(ContactService contactService, ILogger<ContactController> logger)
    {
        _contactService = contactService;
        _logger = logger;  // Inicializa o logger
    }

    // Protegendo a rota com o atributo Authorize
    [HttpGet]
    [Authorize]  // Só usuários autenticados podem acessar essa rota
    public async Task<IActionResult> GetAllContacts()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Use ClaimTypes.NameIdentifier

        if (userIdClaim == null)
        {
            _logger.LogError("User identity claim (nameid) is missing.");  // Log de erro
            return Unauthorized("User identity is not available.");
        }

        // Garantir que o userId é um número válido
        if (!int.TryParse(userIdClaim, out var userId))
        {
            _logger.LogWarning("Invalid user ID: {userIdClaim}", userIdClaim);  // Log de warning
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
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Modificado para usar NameIdentifier

        if (userIdClaim == null)
        {
            _logger.LogError("User identity claim (nameid) is missing.");  // Log de erro
            return Unauthorized("User identity is not available.");
        }

        // Garantir que o userId é um número válido
        if (!int.TryParse(userIdClaim, out var userId))
        {
            _logger.LogWarning("Invalid user ID: {userIdClaim}", userIdClaim);  // Log de warning
            return BadRequest("Invalid user ID.");
        }

        // Adicionar o contato com o userId
        var contact = await _contactService.AddContactForUser(contactDto, userId);  // Cria um contato para o usuário
        _logger.LogInformation("Contact added successfully: {contactId}", contact.Id);  // Log de sucesso
        return CreatedAtAction(nameof(GetAllContacts), new { id = contact.Id }, contact);
    }
}
