using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSBO5G__Szakdolgozat.Services;
using SSBO5G__Szakdolgozat.Helpers;

namespace SSBO5G__Szakdolgozat.Controllers
{
    [Route("[Controller]/")]
    public class HikeController : Controller
    {
        private ApplicationContext context;
        IHikeService hikeService;

        public HikeController(ApplicationContext context, IHikeService service)
        {
            this.context = context;
            hikeService = service;
        }

        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            try
            {
                return new JsonResult(await hikeService.GetAllHike());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {

            try
            {
                return new JsonResult(await hikeService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
