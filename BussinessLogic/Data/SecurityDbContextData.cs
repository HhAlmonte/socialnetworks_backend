using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace BussinessLogic.Data
{
    public class SecurityDbContextData
    {
        public static async Task SeedUserAsync(UserManager<UserEntities> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new UserEntities
                {
                    Name = "Hector",
                    LastName = "Almonte",
                    Email = "Hbalmontess272@gmail.com",
                    Image = "Imagen de prueba",
                    UserName = "Sstewiie"
                };

                await userManager.CreateAsync(user, "Bryan12s#");
            }
        }
    }
}
