﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProject.Controllers
{
    public class DestinationController : Controller
    {
        DestinationManager destinationManager = new DestinationManager(new EfDestinationDal());
        public IActionResult Index()
        {
            var values = destinationManager.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult DestinationDetails(int id)//istenilen id ye gore detaylar
        {
            var values = destinationManager.TGeyByID(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult DestinationDetails(Destination destination)
        {
            return View();
        }
    }
}
