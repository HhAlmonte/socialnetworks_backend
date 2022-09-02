using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Errors;

namespace WebApi.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserManager<UserEntities> _userManager;
        private readonly SignInManager<UserEntities> _signInManager;

        public UserController(UserManager<UserEntities> userManager,
                              SignInManager<UserEntities> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseUserDto>> Login([FromBody]LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if( user == null)
            {
                return Unauthorized(new CodeErrorResponse(401, "El correo ingresado no existe"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if( !result.Succeeded)
            {
                return Unauthorized(new CodeErrorResponse(401, "La contraseña ingresada es incorrecta"));
            }

            return new ResponseUserDto
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Created = user.Created,
                PhoneNumber = user.PhoneNumber,
                Image = user.Image
            };
        }
    }
}
