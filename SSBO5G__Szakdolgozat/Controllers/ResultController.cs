using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Controllers
{
    [Authorize("Bearer")]
    [Route("[Controller]/")]
    public class ResultController : Controller
    {
        IResultService resultService;
        public ResultController(IResultService resultService)
        {
            this.resultService = resultService;
        }

        [AllowAnonymous]
        [HttpGet("result/{courseId}")]
        public async Task<IActionResult> Record(int courseId)
        {
            try
            {
                var result = await resultService.GetResults(courseId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
