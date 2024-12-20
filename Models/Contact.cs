public class Contact
{
    public int Id { get; set; }
    public int UserId { get; set; }  // Relacionamento com o usuário
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Phone { get; set; }

    public int AddressId { get; set; }  // Chave estrangeira para Address
    public Address Address { get; set; }  // Relacionamento com Address
}

public class Address
{
    public int Id { get; set; }  // Adicionando a chave primária
    public string Street { get; set; }
    public string Number { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
}