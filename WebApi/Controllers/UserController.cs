using Core.Entities;
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
        /*Imageprofileformat imageprofileformat = new Imageprofileformat();*/

        public UserController(UserManager<UserEntities> userManager,
                              SignInManager<UserEntities> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseUserDto>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized(new CodeErrorResponse(401, "El correo ingresado no existe"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
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

        [HttpPost("Registration")]
        public async Task<ActionResult<ResponseUserDto>> Registration([FromBody] RegistrationDto registrationDto, IFormFile image)
        {
            var builder = WebApplication.CreateBuilder();
            var connection = builder.Configuration.GetConnectionString("ConnectionContainer");
            var container = builder.Configuration.GetConnectionString("ContainerName");

            /*FileContainer fileContainer = new FileContainer(connection, container);*/

            /*var imageResult = await fileContainer.UploadBlobAsync();*/

            var user = new UserEntities(
                registrationDto.Name,
                registrationDto.LastName,
                registrationDto.Email)
            {
                Image = registrationDto.Image,
                UserName = registrationDto.UserName,
                PhoneNumber = registrationDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registrationDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(400));
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

        /*[HttpPost]
        public async Task<Response> Login(IFormFile form)
        { 
            *//*var builder = WebApplication.CreateBuilder();
            var connection = builder.Configuration.GetConnectionString("ConnectionContainer");
            var container = builder.Configuration.GetConnectionString("ContainerName");

            FileContainer fileContainer = new FileContainer(connection, container);
            
            string ruta = @"C:\Users\Hector Almonte\Downloads\Screenshot_1.jpg";

            return await fileContainer.UploadImage("Prueba", ruta);*//*
        }*/
    }
}
