using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Address> Addresses { get; set; }  // Tabela Address

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurar relacionamento entre Contact e Address
        modelBuilder.Entity<Contact>()
            .HasOne(c => c.Address)           // Um Contact tem um Address
            .WithOne()                         // Um Address pertence a um Contact
            .HasForeignKey<Contact>(c => c.AddressId);  // Chave estrangeira AddressId

        // Configurar relacionamento entre User e Contact
        modelBuilder.Entity<User>()
            .HasMany(u => u.Contacts)          // Um User pode ter muitos Contacts
            .WithOne(c => c.User)              // Cada Contact tem um User
            .HasForeignKey(c => c.UserId);     // Chave estrangeira UserId

        base.OnModelCreating(modelBuilder);
    }
}
