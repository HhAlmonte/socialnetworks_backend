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
                var user = new UserEntities(
                        "Hector",
                        "Almonte",
                        "Hbalmontess272@gmail.com",
                        "Imagen de prueba"
                    )
                { 
                    UserName = "Sstewiie"
                };

                await userManager.CreateAsync(user, "Bryan12s");
            }
        }
    }
}
