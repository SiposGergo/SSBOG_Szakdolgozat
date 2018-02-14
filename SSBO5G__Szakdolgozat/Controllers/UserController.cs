using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SSBO5G__Szakdolgozat.Dtos;
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
    public class UsersController : Controller
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
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Name = user.Name,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]HikerDto hikerDto)
        {
            // map dto to entity
            var user = mapper.Map<Hiker>(hikerDto);

            try
            {
                // save 
                userService.Create(user, hikerDto.Password);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var users = userService.GetAll();
            var userDtos = mapper.Map<IList<HikerDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("user/{id}")]
        public IActionResult GetById(int id)
        {
            var user = userService.GetById(id);
            var userDto = mapper.Map<HikerDto>(user);
            return Ok(userDto);
        }

        [HttpPut("edit/{id}")]
        public IActionResult Update(int id, [FromBody]HikerDto userDto)
        {
            // map dto to entity and set id
            var user = mapper.Map<Hiker>(userDto);
            user.Id = id;

            try
            {
                // save 
                userService.Update(user, userDto.Password);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                // return error message if there was an exception
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
