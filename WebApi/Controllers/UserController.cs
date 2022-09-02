using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Errors;
using Core.Interface;
using Core.Enums;

namespace WebApi.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserManager<UserEntities> _userManager;
        private readonly SignInManager<UserEntities> _signInManager;
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly ITokenService _tokenService;

        public UserController(UserManager<UserEntities> userManager,
                              SignInManager<UserEntities> signInManager,
                              IAzureBlobStorageService azureBlobStorageService,
                              ITokenService tokenService)
        {
            _tokenService = tokenService;
            _azureBlobStorageService = azureBlobStorageService;
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
                Image = user.Image,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("Registration")]
        public async Task<ActionResult<ResponseUserDto>> Registration([FromForm] RegistrationDto registrationDto)
        {
            var user = new UserEntities(
                registrationDto.Name,
                registrationDto.LastName,
                registrationDto.Email);

            user.UserName = registrationDto.UserName;
            user.PhoneNumber = registrationDto.PhoneNumber;

            if (registrationDto.Image != null)
            {
                user.Image = await _azureBlobStorageService.UploadAsync(registrationDto.Image, ContainerEnum.IMAGEPROFILECONTAINER);
            }

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
                Image = user.Image,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}
