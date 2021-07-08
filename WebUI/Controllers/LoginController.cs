using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
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
					return RedirectToAction("Index", "Home");
				else
					return RedirectToAction("Error");
			}
			catch
			{
				return View();
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
