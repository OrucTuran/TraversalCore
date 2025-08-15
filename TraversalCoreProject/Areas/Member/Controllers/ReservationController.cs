using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCoreProject.Areas.Member.Controllers
{
    [Area("Member")]
    public class ReservationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IReservationService _reservationService;

        public ReservationController(UserManager<AppUser> userManager, IReservationService reservationService)
        {
            _userManager = userManager;
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> MyReservations()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Doğru: Include'lu rezervasyon listesini alacak bir servis metodu kullanılmalı
            var reservations = _reservationService.TGetListWithDestination()
                                    .Where(r => r.AppUserID == user.Id && r.ReservationDate >= DateTime.Now)
                                    .OrderByDescending(r => r.ReservationDate)
                                    .ToList();

            return View(reservations);
        }

        [HttpGet]
        public async Task<IActionResult> MyOldReservations()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var oldReservations = _reservationService.TGetListWithDestination()
                .Where(r => r.AppUserID == user.Id && r.ReservationDate < DateTime.Now)
                .OrderByDescending(r => r.ReservationDate)
                .ToList();

            return View(oldReservations);
        }

        [HttpGet]
        public IActionResult NewReservation()
        {
            ViewBag.Destinations = new SelectList(_reservationService.TGetActiveDestinations(), "DestinationID", "City");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewReservation(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                reservation.AppUserID = user.Id;
                reservation.Status = reservation.ReservationDate < DateTime.Now
                    ? ReservationStatus.Past : ReservationStatus.Pending;

                _reservationService.TAdd(reservation);

                TempData["ReservationMessage"] = "Rezervasyonunuz şu anda onay bekliyor. Rezervasyonlarınızın durumunu görmek için 'Rezervasyonlarım' sayfasına gidebilirsiniz.";
                return RedirectToAction("MyReservations");
            }

            // Hata durumunda dropdown tekrar doldur
            ViewBag.Destinations = new SelectList(_reservationService.TGetActiveDestinations(), "DestinationID", "City");
            return View(reservation);
        }
    }
}
