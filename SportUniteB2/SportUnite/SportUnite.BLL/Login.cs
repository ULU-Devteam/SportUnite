using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SportUnite.DAL.Identity;

namespace SportUnite.BLL
{
	public class Login
	{
		public async Task<bool> UserLogin(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
			SignInManager<IdentityUser> signInManager, IdentityUser loginUser)
		{
			var user =
				await userManager.FindByNameAsync(loginUser.UserName);

			if (user == null) return false;
			await signInManager.SignOutAsync();
			return (await signInManager.PasswordSignInAsync(user,
				loginUser.PasswordHash, false, false)).Succeeded;
		}

		public async Task UserLogOut(SignInManager<IdentityUser> signInManager)
		{
			await signInManager.SignOutAsync();
		}

		public async Task<bool> Register(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
			SignInManager<IdentityUser> signInManager, IdentityUser loginUser, string role)
		{
			var manager = new IdentityUserManager(userManager, roleManager, signInManager);
			return await manager.Register(loginUser, role);

		}
	}
}