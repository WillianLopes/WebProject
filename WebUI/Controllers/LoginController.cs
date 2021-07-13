using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Controllers
{
	public class LoginController : Controller
	{
		// GET: LoginController
		public ActionResult Index()
		{
			return View();
		}		

		// POST: LoginController/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginUserViewModel loginUser)
		{
			try
			{
				var response = await Util.DoRequest(HttpMethod.POST, "api/account/login", loginUser);
				
				if (response.StatusCode == HttpStatusCode.OK)
				{
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, loginUser.Email)
					};

					var claimsIdentity = new ClaimsIdentity(claims, "Login");
					var authProperties = new AuthenticationProperties
					{
						AllowRefresh = true,
						IsPersistent = false,
						ExpiresUtc = DateTime.UtcNow.AddHours(2)
					};

					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

					return RedirectToAction("Index", "Home");
				}
				else
					return RedirectToAction("Error");
			}
			catch
			{
				return View();
			}
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();

			return RedirectToAction("Index", "Login");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
