using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SSBO5G__Szakdolgozat.Dtos;
using SSBO5G__Szakdolgozat.Exceptions;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Controllers
{
    [Authorize("Bearer")]
    [Route("[controller]")]
    public class UsersController : MyController
    {
        private IUserService userService;
        private IMapper mapper;
        private readonly AppSettings appSettings;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.appSettings = appSettings.Value;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]HikerDto hikerDto)
        {
            var user = userService.Authenticate(hikerDto.UserName, hikerDto.Password);

            if (user == null)
                return BadRequest("A felahsználónév vagy jelszó helytelen.");
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            
            return Ok(new
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Town = user.Town,
                PhoneNumber = user.PhoneNumber,
                user.mustChangePassword,
                Token = tokenString,
                registrations = mapper.Map<IEnumerable<RegistrationDto>>(user.Registrations)
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]HikerDto hikerDto)
        {
            var user = mapper.Map<Hiker>(hikerDto);

            try
            {
                userService.Create(user, hikerDto.Password);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var users = await userService.GetAll();
            var userDtos = mapper.Map<IList<HikerDto>>(users);
            return Ok(userDtos);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            try
            {
                var userId = GetLoggedInUserId();
                await userService.ChangePassword(userId, dto);
                return Ok();
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
        [HttpPost("forgotten-password")]
        public async Task<IActionResult> ForgottenPassword([FromBody] ForgottenPasswordDto dto)
        {
            try
            {
                await userService.ForgottenPassword(dto);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{id}")]
        public async Task <IActionResult> GetById(int id)
        {
            try
            {
                var user = await userService.GetById(id);
                var userDto = mapper.Map<HikerDto>(user);
                return Ok(userDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [HttpPut("edit/{id}")]
        public IActionResult Update(int id, [FromBody]HikerDto userDto)
        {
            var user = mapper.Map<Hiker>(userDto);
            int loggedInUserId = GetLoggedInUserId();
            if (loggedInUserId != id)
            {
                return Forbid();
            }
            user.Id = id;

            try
            {
                userService.Update(user, userDto.Password);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            userService.Delete(id);
            return Ok();
        }
    }
}
