using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider, IHostEnvironment env)
    {
        using var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());
        
        // Adicionar dados de teste se necessário
        if (!context.Users.Any())
        {
            context.Users.Add(new User { FullName = "Admin", Email = "admin@example.com", PasswordHash = "hashedpassword" });
            context.SaveChanges();
        }
    }
}
