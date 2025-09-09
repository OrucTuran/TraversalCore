using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GuideController : Controller
    {
        private readonly IGuideService _guideService;

        public GuideController(IGuideService guideService)
        {
            _guideService = guideService;
        }

        public IActionResult Index()
        {
            var values = _guideService.TGetList();
            return View(values);
        }

        public IActionResult ChangeStatus(int id)
        {
            var guide = _guideService.TGeyByID(id);
            if (guide != null)
            {
                guide.Status = !guide.Status; // aktifse pasif yap, pasifse aktif
                _guideService.TUpdate(guide);
            }
            return RedirectToAction("Index");
        }
    }
}
