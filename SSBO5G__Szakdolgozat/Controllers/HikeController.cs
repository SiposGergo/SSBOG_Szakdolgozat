using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSBO5G__Szakdolgozat.Services;
using SSBO5G__Szakdolgozat.Helpers;
using AutoMapper;
using SSBO5G__Szakdolgozat.Dtos;

namespace SSBO5G__Szakdolgozat.Controllers
{
    [Route("[Controller]/")]
    public class HikeController : Controller
    {
        private ApplicationContext context;
        private IMapper mapper;
        IHikeService hikeService;

        public HikeController(ApplicationContext context, IHikeService service,IMapper mapper)
        {
            this.context = context;
            hikeService = service;
            this.mapper = mapper;
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
                var hike = await hikeService.GetById(id);
                var hikeDto = mapper.Map<HikeDto>(hike);
                return new JsonResult(hikeDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
