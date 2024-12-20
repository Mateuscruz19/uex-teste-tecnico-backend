public class Contact
{
    public int Id { get; set; }
    public int UserId { get; set; }  // Relacionamento com o usuário
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Phone { get; set; }
    public Address Address { get; set; }  // Endereço do contato
}
public class Address
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
}
