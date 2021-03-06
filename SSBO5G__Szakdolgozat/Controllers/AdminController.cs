﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSBO5G__Szakdolgozat.Dtos;
using SSBO5G__Szakdolgozat.Exceptions;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Controllers
{
    [Authorize("Bearer")]
    [Route("[Controller]/")]
    public class AdminController : MyController
    {
        private readonly IMapper mapper;
        private readonly IAdminService adminService;

        public AdminController(IMapper mapper, IAdminService adminService)
        {
            this.mapper = mapper;
            this.adminService = adminService;
        }
        
        [HttpPost("record-checkpoint-pass")]
        public async Task<IActionResult> Record([FromBody] RecordDto recordDto)
        {
            try
            {
                int loggedInUserId = GetLoggedInUserId();
                string message = await adminService.RecordCheckpointPass(loggedInUserId, recordDto);
                return Ok(new { message });
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

    }
}
