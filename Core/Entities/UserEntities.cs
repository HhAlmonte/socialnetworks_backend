using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class UserEntities : IdentityUser
    {
        public UserEntities(string name,
                            string lastName,
                            string email,
                            string? image)
        {
            Name = name;
            LastName = lastName;
            Image = image;
            Email = email;
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Image { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
