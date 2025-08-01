using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProject.Controllers
{
    public class LoginController : Controller
    {
		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}
		//[HttpPost]
		//public IActionResult SignUp()
		//{
		//    return View();
		//}

		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public IActionResult SignIn(int id)
		{
			return View();
		}
	}
}
