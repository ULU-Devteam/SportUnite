using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportUnite.BLL;
using SportUnite.Domain.Models.Identity;

namespace SportUnite.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public HomeController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
			SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
		}

		public ViewResult Index()
		{
			ViewBag.Title = "SportUnite";

			return View();
		}

		public async Task<ViewResult> Register()
		{
			var manager = new Login();
			var user = new IdentityUser { UserName = "Gied", PasswordHash = "Gied123$" };
			await manager.Register(_userManager, _roleManager, _signInManager, user, "ComplexManager");

			ViewBag.Title = "SportUnite";

			return View("Index");
		}

		[HttpGet]
		[AllowAnonymous]
		public ViewResult Login()
		{
			ViewBag.Title = "SportUnite";

			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginModel user)
		{

			if (ModelState.ContainsKey("Email") && ModelState.ContainsKey("Role"))
			{
				ModelState["Email"].ValidationState = ModelValidationState.Valid;
				ModelState["Role"].ValidationState = ModelValidationState.Valid;
			}

			if (!ModelState.IsValid) return View();

			var logUser = new IdentityUser { UserName = user.Name, PasswordHash = user.Password };
			var login = new Login();
			var result = await login.UserLogin(_userManager, _roleManager, _signInManager, logUser);

			if (result) return Redirect(user.ReturnUrl ?? "/Admin/Dashboard");
			ModelState.AddModelError("", "Invalid name or password");

			return View();
		}

		public async Task<ViewResult> Logout()
		{
			var login = new Login();
			await login.UserLogOut(_signInManager);

			return View("Login");
		}
	}
}
