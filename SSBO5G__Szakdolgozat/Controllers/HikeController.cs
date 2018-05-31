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
using SSBO5G__Szakdolgozat.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SSBO5G__Szakdolgozat.Controllers
{
    [Authorize("Bearer")]
    [Route("[Controller]/")]
    public class HikeController : Controller
    {
        private ApplicationContext context;
        private IMapper mapper;
        IHikeService hikeService;
        IRegistrationService registrationService;

        public HikeController(ApplicationContext context, IHikeService service,IMapper mapper, 
            IRegistrationService registrationService)
        {
            this.context = context;
            hikeService = service;
            this.mapper = mapper;
            this.registrationService = registrationService;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {

            try
            {
                var hike = await hikeService.GetById(id);
                var hikeDto = mapper.Map<HikeDto>(hike);
                return Ok(hikeDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("comment")]
        public async Task<IActionResult> Comment([FromBody]CommentDto commentDto)
        {
            Comment comment = mapper.Map<Comment>(commentDto);
            try
            {
                Comment c = await hikeService.AddCommentToHike(comment);
                CommentDto dto = mapper.Map<CommentDto>(c);
                return Ok(dto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPut("register")]
        public async Task<IActionResult> Register([FromBody]Registration registration)
        {
            try
            {
                Registration reg = await registrationService.RegisterToHike(registration);
                RegistrationDto registrationDto1 = mapper.Map<RegistrationDto>(reg);
                return Ok(registrationDto1);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("unregister")]
        public async Task<IActionResult> UnRegister([FromBody]Registration registration)
        {
            try
            {
                int id = await registrationService.UnRegisterFromHike(registration);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] HikeDto hikeDto)
        {
            try
            {
                string userId = HttpContext.User.FindFirstValue(ClaimTypes.Name);
                int id = 0;
                if (userId == null || userId == "" || !Int32.TryParse(userId,out id))
                {
                    return BadRequest("Felhasználó nem található!");
                }
                Hike hike = mapper.Map<Hike>(hikeDto);
                hike.OrganizerId = id;
                await hikeService.AddHike(hike);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}