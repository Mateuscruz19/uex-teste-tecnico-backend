namespace YourNamespace.Dtos
{
    public class UserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Recebe a senha antes de hash
    }
}
