using BusinessLayer.Abstract;
using EntityLayer.Concrete;
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
                guide.Status = !guide.Status;
                _guideService.TUpdate(guide);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddGuide()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGuide(Guide guide)
        {
            _guideService.TAdd(guide);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult EditGuide(int id)
        {
            var value = _guideService.TGeyByID(id);

            return View(value);
        }

        [HttpPost]
        public IActionResult EditGuide(Guide guide)
        {
            _guideService.TUpdate(guide);
            return RedirectToAction(nameof(Index));
        }
    }
}
