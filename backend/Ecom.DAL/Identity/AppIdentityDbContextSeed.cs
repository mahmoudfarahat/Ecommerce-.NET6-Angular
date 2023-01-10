using Ecom.BLL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.DAL.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Ahmed",
                    Email = "ahmed@gmail.com",
                    UserName = "ahmed@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Ahmed",
                        LastName = "Khaled",
                        Street = "10 st 9 Maadi",
                        City = "Maadi",
                        State = "Cairo",
                        ZipCode = "123456"
                    }
                };
                await userManager.CreateAsync(user);
            }    
        }
    }
}
