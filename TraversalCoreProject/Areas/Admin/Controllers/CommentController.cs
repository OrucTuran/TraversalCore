using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            var values = _commentService.TGetCommentsWithDestination();
            return View(values);
        }
        public IActionResult DeleteComment(int id)
        {
            var values = _commentService.TGeyByID(id);
            if (values == null)
            {
                return Json(new { success = false, message = "Yorum bulunamadı." });
            }

            _commentService.TDelete(values);
            return Json(new { success = true, message = "Yorum başarıyla silindi." });

        }
    }
}
