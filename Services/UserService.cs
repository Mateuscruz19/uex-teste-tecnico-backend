using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Dtos;
using BCrypt.Net;

public class UserService
{
    private readonly AppDbContext _dbContext;

    public UserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> Register(UserDto userDto)
    {
        if (_dbContext.Users.Any(u => u.Email == userDto.Email))
            throw new Exception("Email já está em uso.");

        var user = new User
        {
            FullName = userDto.FullName,
            Email = userDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            throw new Exception("Credenciais inválidas.");

        // Gerar um token JWT (implementação fictícia, você precisará ajustar isso)
        return "token_fake_de_exemplo";
    }

}
