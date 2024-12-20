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

        base.OnModelCreating(modelBuilder);
    }
}
