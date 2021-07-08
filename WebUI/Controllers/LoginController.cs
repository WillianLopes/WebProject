using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
		public ActionResult Login(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}		
	}
}
