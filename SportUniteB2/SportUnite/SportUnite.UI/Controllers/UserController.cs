using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SportUnite.UI.Models;
using SportUnite.BLL;
using SportUnite.Domain.Models.Identity;

namespace SportUnite.UI.Controllers
{

	public class UserController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
			SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
		}

		[Authorize(Roles = "Administrator")]
		public ViewResult UserList()
		{
			ViewBag.Title = "Gebruikers";
			var model = new List<UserViewModel>();
			foreach (var u in _userManager.Users)
			{
				var usermodel = new UserViewModel
				{
					User = u,
					Roles = _userManager.GetRolesAsync(_userManager.FindByIdAsync(u.Id).Result).Result
				};
				model.Add(usermodel);
			}

			return View(model);
		}

		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public ViewResult AddUser()
		{
			ViewBag.Title = "Gebruiker toevoegen";

			return View();
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public async Task<IActionResult> AddUser(LoginModel model)
		{
			ViewBag.Title = "Gebruiker toevoegen";

			if (!ModelState.IsValid) return View();

			var manager = new Login();
			var user = new IdentityUser { UserName = model.Name, PasswordHash = model.Password, Email = model.Email, PhoneNumber = model.Phone };

			var result = await manager.Register(_userManager, _roleManager, _signInManager, user, model.Role);

			if (result) return RedirectToAction("UserList");

			ModelState.AddModelError("", "Add user failed");
			return View();
		}

		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public ViewResult UpdateUser(IdentityUser user)
		{
			ViewBag.Title = "Gebruiker wijzigen";
			return View(user);
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public IActionResult UpdateUserPost(IdentityUser user)
		{
			ViewBag.Title = "Gebruiker wijzigen";

			if (!ModelState.IsValid) return View("UpdateUser", user);

			var updateUser = _userManager.FindByIdAsync(user.Id).Result;

			updateUser.UserName = user.UserName;
			updateUser.Email = user.Email;
			updateUser.PhoneNumber = user.PhoneNumber;

			var result = _userManager.UpdateAsync(updateUser).Result;

			if (result.Succeeded) return RedirectToAction("UserList");

			ModelState.AddModelError("", "Remove user failed");
			return RedirectToAction("UserList");
		}

		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> DeleteUser(IdentityUser user)
		{
			if (user.UserName.Equals("Admin")) return RedirectToAction("UserList");

			var result = await _userManager.DeleteAsync(user);

			if (result.Succeeded) return RedirectToAction("UserList");

			ModelState.AddModelError("", "Remove user failed");
			return RedirectToAction("UserList");
		}
	}
}