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
    public class HikeController : MyController 
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

        [AllowAnonymous]
        [HttpGet("today")]
        public async Task<IActionResult> TodayHikes()
        {
            try
            {
                var hikes = await hikeService.GetTodayHikes();
                var hikesDto = mapper.Map<IEnumerable<HikeDto>>(hikes);
                return Ok(hikesDto);
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

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] HikeDto hikeDto)
        {
            try
            {
                int id = GetLoggedInUserId();
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

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] HikeDto hikeDto)
        {
            try
            {
                int id = GetLoggedInUserId();
                Hike hike = mapper.Map<Hike>(hikeDto);
                await hikeService.EditHike(hike, id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("add-helper/{hikeId}")]
        public async Task<IActionResult> AddHelper(int hikeId, [FromBody] UserNameDto userNameDto)
        {
            try
            {
                int loggedInUserId = GetLoggedInUserId();
                await hikeService.AddHelper(hikeId, loggedInUserId,userNameDto.UserName);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}