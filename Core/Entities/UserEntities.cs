using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class UserEntities : IdentityUser
    {
        public UserEntities(string name,
                            string lastName,
                            string email,
                            string userName,
                            string? image = null,
                            string? phoneNumber = null)
        { 
            Name = name;
            LastName = lastName;
            Email = email;
            UserName = userName;
            Image = image ?? "https://socialnetworkscontainer.blob.core.windows.net/imageprofilecontainer/DefaultImage.jpg";
            PhoneNumber = phoneNumber;
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Image { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
