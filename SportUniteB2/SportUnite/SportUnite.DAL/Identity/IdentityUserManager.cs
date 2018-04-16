using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SportUnite.DAL.Identity
{
	public class IdentityUserManager
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public IdentityUserManager(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
		}

		public async Task SignOut()
		{
			await _signInManager.SignOutAsync();
		}

		public async Task<bool> Register(IdentityUser modelUser)
		{
			var user = new IdentityUser { UserName = modelUser.UserName, PasswordHash = modelUser.PasswordHash };
			var result = await _userManager.CreateAsync(user, modelUser.PasswordHash);

			return result.Succeeded;
		}

		public async Task<bool> AddEditApplicationRole(IdentityRole role)
		{
			var isExist = !string.IsNullOrEmpty(role.Id);
			var identityRole = isExist ? await _roleManager.FindByIdAsync(role.Id) :
				new IdentityRole();

			var roleResult = isExist ? await _roleManager.UpdateAsync(identityRole) :
				await _roleManager.CreateAsync(identityRole);

			return roleResult.Succeeded;
		}

		public async Task<bool> Register(IdentityUser user, string role)
		{
			var result = await _userManager.CreateAsync(user, user.PasswordHash);

			if (!result.Succeeded) return false;

			var currentUser = await _userManager.FindByNameAsync(user.UserName);
			var roles = new List<string> { role };
			await _userManager.AddToRolesAsync(currentUser, roles);
			await _userManager.UpdateSecurityStampAsync(user);

			return true;
		}
	}
}
