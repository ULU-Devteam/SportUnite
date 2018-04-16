using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SportUnite.DAL.Identity
{
    public static class SeedDataIdentity
    {
            private const string AdminUser = "Admin";
            private const string AdminPassword = "Admin123$";

            public static async void EnsurePopulated(IApplicationBuilder app)
            {
                UserManager<IdentityUser> userManager = app.ApplicationServices
                    .GetRequiredService<UserManager<IdentityUser>>();

                RoleManager<IdentityRole> roleManager =
                    app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();

                IdentityRole adminRole = new IdentityRole{Name = "Administrator"};
                IdentityRole complexManagerRole = new IdentityRole { Name = "ComplexManager" };

                await roleManager.CreateAsync(adminRole);
                await roleManager.CreateAsync(complexManagerRole);

                

            var user = await userManager.FindByIdAsync(AdminUser);
                if (user != null) return;
                user = new IdentityUser {UserName = "Admin", Email = "admin@sportunite.nl"};
                await userManager.CreateAsync(user, AdminPassword);
                await userManager.AddToRoleAsync(user, "Administrator");
                await userManager.UpdateSecurityStampAsync(user);

        }
    }
}
