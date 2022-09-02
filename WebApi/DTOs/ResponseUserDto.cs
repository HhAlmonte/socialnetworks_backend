namespace WebApi.DTOs
{
    public class ResponseUserDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Image { get; set; } 
        public DateTime Created { get; set; }
        public string? Token { get; set; }
    }
}
