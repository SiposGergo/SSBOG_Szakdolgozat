using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSBO5G__Szakdolgozat.Dtos;
using SSBO5G__Szakdolgozat.Exceptions;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Controllers
{
    [Authorize("Bearer")]
    [Route("[Controller]/")]
    public class CourseController : MyController
    {
        private readonly IMapper mapper;
        private readonly ICourseService courseService;

        public CourseController(IMapper mapper, ICourseService courseService)
        {
            this.mapper = mapper;
            this.courseService = courseService;
        }
        
        [HttpPut("add/{hikeId}")]
        public async Task<IActionResult> AddCourse(int hikeId, [FromBody] HikeCourseDto hikeCourseDto)
        {
            try
            {
                int userId = GetLoggedInUserId();
                HikeCourse hikeCourse = mapper.Map<HikeCourse>(hikeCourseDto);
                await courseService.AddCourse(hikeCourse, userId, hikeId);
                return Ok();
            }
            catch (UnauthorizedException)
            {
                return Forbid();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("details/{courseId}")]
        public async Task<IActionResult> Details(int courseId)
        {
            try
            {
                HikeCourse course = await courseService.GetCourse(courseId);
                return Ok(mapper.Map<HikeCourseDto>(course));
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("edit/{courseId}")]
        public async Task<IActionResult> Edit(int courseId, [FromBody]HikeCourseDto courseDto)
        {
            try
            {
                int userId = GetLoggedInUserId();
                HikeCourse course = mapper.Map<HikeCourse>(courseDto);
                await courseService.UpdateCourse(userId, courseId, course);
                return Ok();
            }
            catch (UnauthorizedException)
            {
                return Forbid();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("pdf-info/{courseId}")]
        public async Task<IActionResult> GetPdfInfo(int courseId)
        {
            try
            {
                int loggedInUserId = GetLoggedInUserId();
                byte[] result = await courseService.GetPdfCourseInfo(courseId, loggedInUserId);
                return File(result, "application/pdf", "Nevezettek.pdf");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
