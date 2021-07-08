using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Controllers
{
	public class RegisterUserController : Controller
	{
		// GET: RegisterUserController
		public ActionResult Index()
		{
			return View();
		}

		// GET: RegisterUserController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: RegisterUserController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: RegisterUserController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(RegisterUserViewModel registerUser)
		{
			try
			{
				var response = await Util.DoRequest(HttpMethod.POST, "api/account/register", registerUser);

				string apiResponse = await response.Content.ReadAsStringAsync();

				return RedirectToAction("Index", "Login");
			}
			catch
			{
				return View();
			}
		}

		// GET: RegisterUserController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: RegisterUserController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
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

		// GET: RegisterUserController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: RegisterUserController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
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
