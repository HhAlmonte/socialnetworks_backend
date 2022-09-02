namespace WebApi.DTOs
{
    public class RegistrationDto
    {
        public RegistrationDto(string name, string lastName, string email)
        {
            Name = name;
            LastName = lastName;
            Email = email;
        }

        public string Name { get; set; }
        
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? Image { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
