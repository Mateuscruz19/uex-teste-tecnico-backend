[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly ContactService _contactService;

    public ContactController(ContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContacts()
    {
        var contacts = await _contactService.GetAllContacts();
        return Ok(contacts);
    }

    [HttpPost]
    public async Task<IActionResult> AddContact([FromBody] ContactDto contactDto)
    {
        var contact = await _contactService.AddContact(contactDto);
        return CreatedAtAction(nameof(GetAllContacts), new { id = contact.Id }, contact);
    }

    // Métodos de editar e excluir contato
}
