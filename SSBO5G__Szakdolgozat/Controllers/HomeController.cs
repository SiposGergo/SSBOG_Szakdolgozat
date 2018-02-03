using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSBO5G__Szakdolgozat.Models;

namespace SSBO5G__Szakdolgozat.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext context;
        public HomeController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var hikes = context.Hikes.ToList();
            return new JsonResult(hikes);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
    }
}
