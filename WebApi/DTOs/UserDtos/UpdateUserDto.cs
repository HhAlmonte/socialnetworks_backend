using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.UserDtos
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public IFormFile Image { get; set; }

        public class NewPasswordDto
        {
            [Required]
            public string OldPassword { get; set; }
            [Required]
            public string NewPassword { get; set; }
            [Required]
            public string ConfirmPassword { get; set; }
        }

        public class NewEmailDto
        {
            [Required]
            public string OldEmail { get; set; }
            [Required]
            public string NewEmail { get; set; }
            [Required]
            public string ConfirmEmail { get; set; }
        }
    }
}
