using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IReservationService _reservationService;

        public UserController(IAppUserService appUserService, UserManager<AppUser> userManager, IReservationService reservationService)
        {
            _appUserService = appUserService;
            _userManager = userManager;
            _reservationService = reservationService;
        }

        public IActionResult Index()
        {
            var values = _appUserService.TGetList();
            return View(values);
        }
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var value = _appUserService.TGeyByID(id);
                if (value == null)
                {
                    return Json(new { success = false, message = "Kullanıcı bulunamadı!" });
                }

                _appUserService.TDelete(value);
                return Json(new { success = true, message = "Kullanıcı başarıyla silindi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Silme işlemi sırasında hata oluştu: " + ex.Message });
            }
        }
        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var value = _appUserService.TGeyByID(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult EditUser(AppUser appUser)
        {
            _appUserService.TUpdate(appUser);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CommentUser(int id)
        {
            _appUserService.TGetList();
            return View();
        }
        public async Task<IActionResult> ReservationUser(int id)//kullanicinin gecmiste kayit oldugu turlar icin
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var oldReservations = _reservationService.TGetListWithDestination()
                .Where(r => r.AppUserID == user.Id && r.ReservationDate < DateTime.Now)
                .OrderByDescending(r => r.ReservationDate)
                .ToList();

            foreach (var item in oldReservations)
            {
                item.Status = ReservationStatus.Past;
            }

            return View(oldReservations);
        }
    }
}