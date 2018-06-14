using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSBO5G__Szakdolgozat.Dtos;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Controllers
{
    [Authorize("Bearer")]
    [Route("[Controller]/")]
    public class AdminController : MyController
    {
        public AdminController(IMapper mapper, IAdminService adminService)
        {
            this.mapper = mapper;
            this.adminService = adminService;
        }
        IMapper mapper;
        IAdminService adminService;

        [HttpPost("record-checkpoint-class")]
        public async Task<IActionResult> Record([FromBody] RecordDto recordDto)
        {
            try
            {
                int loggedInUserId = GetLoggedInUserId();
                return Json (new {
                    message = await adminService.RecordCheckpointPass(loggedInUserId, recordDto)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
