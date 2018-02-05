using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSBO5G__Szakdolgozat.Models;
using Microsoft.EntityFrameworkCore;

namespace SSBO5G__Szakdolgozat.Controllers
{
    [Route("[Controller]/")]
    public class HikeController : Controller
    {
        private ApplicationContext context;
        public HikeController(ApplicationContext context)
        {
            this.context = context;
        }

        [HttpGet("all")]
        public IActionResult All()
        {
            var hikes = context.Hikes
                .Include(x=>x.Courses)
                .ToListAsync();
            return new JsonResult(hikes);
        }
    }
}
