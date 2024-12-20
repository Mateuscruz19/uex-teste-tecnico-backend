using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using YourNamespace.Dtos;
public class UserService
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public UserService(AppDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    private bool IsValidEmail(string email)
    {
        var emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
        return emailRegex.IsMatch(email);
    }

    private bool IsValidPassword(string password)
    {
        var passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        return passwordRegex.IsMatch(password);
    }

    public async Task<User> Register(UserDto userDto)
    {
        if (!IsValidEmail(userDto.Email))
            throw new ArgumentException("O e-mail fornecido não é válido.");

        if (!IsValidPassword(userDto.Password))
            throw new ArgumentException("A senha deve conter pelo menos 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais.");

        if (_dbContext.Users.Any(u => u.Email == userDto.Email))
            throw new ArgumentException("Email já está em uso.");

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
            throw new UnauthorizedAccessException("Credenciais inválidas.");

        // Gerar o Token JWT
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = _configuration["Jwt:SecretKey"] ?? throw new Exception("Secret key not found in configuration.");
        var key = Encoding.ASCII.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[] {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.FullName),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<bool> DeleteUserAndContacts(int userId)
    {
        var user = await _dbContext.Users.Include(u => u.Contacts)
                                          .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return false; // Usuário não encontrado
        }

        // Deleta os contatos do usuário
        _dbContext.Contacts.RemoveRange(user.Contacts);

        // Deleta o usuário
        _dbContext.Users.Remove(user);

        await _dbContext.SaveChangesAsync();
        return true; // Deleção bem-sucedida
    }
}
