using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Dtos;

public class ContactService
{
    private readonly AppDbContext _dbContext;

    public ContactService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Método para buscar os contatos do usuário autenticado
    public async Task<List<Contact>> GetAllContactsForUser(int userId)
    {
        return await _dbContext.Contacts
            .Where(c => c.UserId == userId)  // Filtra os contatos apenas para o usuário logado
            .Include(c => c.Address)
            .ToListAsync();
    }

    // Método para adicionar um contato ao usuário autenticado
    public async Task<Contact> AddContactForUser(ContactDto contactDto, int userId)
    {
        var contact = new Contact
        {
            UserId = userId,  // Relaciona o contato ao usuário autenticado
            Name = contactDto.Name,
            Cpf = contactDto.Cpf,
            Phone = contactDto.Phone,
            Address = new Address
            {
                Street = contactDto.Street,
                Number = contactDto.Number,
                Neighborhood = contactDto.Neighborhood,
                City = contactDto.City,
                State = contactDto.State,
                Latitude = contactDto.Latitude,
                Longitude = contactDto.Longitude
            }
        };

        _dbContext.Contacts.Add(contact);
        await _dbContext.SaveChangesAsync();

        return contact;
    }
}
