using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using TraversalCoreProject.Areas.Member.Models;

namespace TraversalCoreProject.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public ProfileController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditViewModel userEditViewModel = new UserEditViewModel
            {
                Name = values.Name,
                Surname = values.Surname,
                PhoneNumber = values.PhoneNumber,
                Email = values.Email,
                ImageURL = values.ImageURL,
            };
            return View(userEditViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                bool passwordChanged = false;

                user.Name = model.Name;
                user.Surname = model.Surname;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;

                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    // Yeni sifre, mevcut sifre hash'inden farklı mı kontrol edelim
                    var passwordVerification = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
                    if (passwordVerification != PasswordVerificationResult.Success)
                    {
                        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                        passwordChanged = true;
                    }
                }

                // Resim yükleme islemi
                if (model.Image != null && model.Image.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserImages", fileName);

                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }

                    user.ImageURL = "/UserImages/" + fileName;
                }

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if (passwordChanged)
                    {
                        await _signInManager.SignOutAsync();
                        return RedirectToAction("SignIn", "Login");
                    }
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

    }
}