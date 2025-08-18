using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TraversalCoreProject.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]
    public class NewslatterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public NewslatterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> ToggleSubscription([FromBody] bool isSubscribed)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Json(new { success = false, message = "Kullanıcı bulunamadı!" });

            user.IsSubscribed = isSubscribed;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Json(new
                {
                    success = true,
                    message = isSubscribed ? "Bültene abone oldunuz" : "Bülten aboneliğiniz iptal edildi"
                });

            return Json(new { success = false, message = "Bir hata oluştu!" });
        }
    }
}
