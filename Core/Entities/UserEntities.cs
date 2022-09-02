using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class UserEntities : IdentityUser
    {
        public UserEntities(string name,
                            string lastName,
                            string email,
                            string? image = null,
                            string? userName = null,
                            string? phoneNumber = null)
        { 
            Name = name;
            LastName = lastName;
            Email = email;
            Image = image ?? "No se ha ingresado imagen para este usuario";
            UserName = userName ?? "No se ha ingresado nombre de usuario para este usuario";
            PhoneNumber = phoneNumber ?? "No se ha ingresado un número telefónico para este usuario";
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
