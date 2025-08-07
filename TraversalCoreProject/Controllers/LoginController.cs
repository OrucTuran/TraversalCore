using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TraversalCoreProject.Models;

namespace TraversalCoreProject.Controllers
{

    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public LoginController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterViewModel userRegisterViewModel)
        {
            AppUser appUser = new AppUser()
            {
                Name = userRegisterViewModel.Name,
                Surname = userRegisterViewModel.Surname,
                Email = userRegisterViewModel.Email,
                UserName = userRegisterViewModel.Username,
                Phone = userRegisterViewModel.PhoneNumber
            };
            if (userRegisterViewModel.Password == userRegisterViewModel.ConfrimPassword)
            {
                var resutl = await _userManager.CreateAsync(appUser, userRegisterViewModel.Password);
                if (resutl.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                else
                {
                    foreach (var item in resutl.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(userRegisterViewModel);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult SignIn(int id)
        //{
        //	return View();
        //}
    }
}
