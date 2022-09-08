using System.ComponentModel.DataAnnotations;
using WebApi.Exceptions.ValidationAttributes;

namespace WebApi.DTOs
{
    public class RegistrationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string UserName { get; set; }

        /*[MaxFileSize(1 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile? Image { get; set; }*/
        
        public string? Image { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
