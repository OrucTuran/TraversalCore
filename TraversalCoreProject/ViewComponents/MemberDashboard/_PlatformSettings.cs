using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TraversalCoreProject.ViewComponents.MemberDashboard
{
    public class _PlatformSettings : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}