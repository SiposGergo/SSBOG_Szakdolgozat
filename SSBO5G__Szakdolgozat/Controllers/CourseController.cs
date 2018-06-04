using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSBO5G__Szakdolgozat.Dtos;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Controllers
{
    [Authorize("Bearer")]
    [Route("[Controller]/")]
    public class CourseController : MyController
    {
        public CourseController(IMapper mapper, ICourseService courseService)
        {
            this.mapper = mapper;
            this.courseService = courseService;
        }
        IMapper mapper;
        ICourseService courseService;

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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("details/{courseId}")]
        public async Task<IActionResult> Details(int courseId)
        {
            try
            {
                HikeCourse course =await courseService.GetCourse(courseId);
                return Ok(mapper.Map<HikeCourseDto>(course));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
