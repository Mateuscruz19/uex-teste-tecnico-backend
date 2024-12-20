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

    public async Task<List<Contact>> GetAllContacts()
    {
        return await _dbContext.Contacts.Include(c => c.Address).ToListAsync();
    }

    public async Task<Contact> AddContact(ContactDto contactDto)
    {
        var contact = new Contact
        {
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
