using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSBO5G__Szakdolgozat.Dtos;
using SSBO5G__Szakdolgozat.Exceptions;
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
    public class RegisterController : Controller
    {

        private readonly IRegistrationService registrationService;
        private readonly IMapper mapper;

        public RegisterController(IMapper mapper, IRegistrationService registrationService)
        {
            this.mapper = mapper;
            this.registrationService = registrationService;
        }

        [HttpPut("register")]
        public async Task<IActionResult> Register([FromBody]Registration registration)
        {
            try
            {
                Registration reg = await registrationService.RegisterToHike(registration);
                RegistrationDto registrationDto = mapper.Map<RegistrationDto>(reg);
                return Ok(registrationDto);
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

        [HttpPut("unregister")]
        public async Task<IActionResult> UnRegister([FromBody]Registration registration)
        {
            try
            {
                int id = await registrationService.UnRegisterFromHike(registration);
                return Ok(id);
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
