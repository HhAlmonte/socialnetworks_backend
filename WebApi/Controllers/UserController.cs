using Core.Entities;
using Core.Enums;
using Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.DTOs;
using WebApi.Errors;
using static WebApi.DTOs.UpdateUserDto;

namespace WebApi.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserManager<UserEntities> _userManager;
        private readonly SignInManager<UserEntities> _signInManager;
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher<UserEntities> _passwordHasher;

        public UserController(UserManager<UserEntities> userManager,
                              SignInManager<UserEntities> signInManager,
                              IAzureBlobStorageService azureBlobStorageService,
                              ITokenService tokenService,
                              IPasswordHasher<UserEntities> passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _azureBlobStorageService = azureBlobStorageService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseUserDto>> Login([FromForm] LoginDto loginDto)
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
                registrationDto.Email,
                registrationDto.UserName);

            user.PhoneNumber = registrationDto.PhoneNumber;
            user.Image = registrationDto.Image;
            
            /*if (registrationDto.Image != null)
            {
                user.Image = await _azureBlobStorageService.UploadAsync(registrationDto.Image, ContainerEnum.IMAGEPROFILECONTAINER);
            }*/

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

        [Authorize]
        [HttpGet("GetUsuario")]
        public async Task<ActionResult<ResponseUserDto>> GetUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound(new CodeErrorResponse(401, "Obtuvimos obteniendo la información del usuario. Por favor intenta otra vez"));
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

        [Authorize]
        [HttpPut("NewPassword")]
        public async Task<ActionResult<string>> UpdatePassword([FromForm] NewPasswordDto newPasswordDto)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await _userManager.FindByEmailAsync(email);

            var oldPassword = await _signInManager.CheckPasswordSignInAsync(user, newPasswordDto.OldPassword, false);

            if (!oldPassword.Succeeded)
            {
                return Unauthorized(new CodeErrorResponse(401, "La contraseña ingresada es incorrecta"));
            }

            var newPassword = newPasswordDto.NewPassword;
            var confirmPassword = newPasswordDto.ConfirmPassword;

            if (!newPassword.Equals(confirmPassword))
            {
                return Unauthorized(new CodeErrorResponse(401, "NewPassword y ConfirmPassword no coinciden"));
            }

            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(500, "Hubo un error actualizando la contraseña. Por favor intenta de nuevo"));
            }

            return "La contraseña ha sido actualizada. Pruebe iniciando sesión nuevamente con el usuario logueado.";
        }

        [Authorize]
        [HttpPut("NewEmail")]
        public async Task<ActionResult<string>> UpdateEmail([FromForm] NewEmailDto newEmailDto)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await _userManager.FindByEmailAsync(email);

            var oldEmail = newEmailDto.OldEmail;

            if (oldEmail != email)
            {
                return Unauthorized(new CodeErrorResponse(401, "El correo ingresado es incorrecto"));
            }

            var newEmail = newEmailDto.NewEmail;
            var confirmEmail = newEmailDto.ConfirmEmail;

            if (newEmail != confirmEmail)
            {
                return Unauthorized(new CodeErrorResponse(401, "newEmail y confirmEmail no coincide"));
            }

            user.Email = newEmail;

            var result = await _userManager.UpdateAsync(user);
            await _userManager.UpdateNormalizedEmailAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(500, "Hubo un error cambiando el correo. Intenta otra vez"));
            }

            return "El correo ingresada fue cambiada. En tu proximo inicio de sesión ingresa tu nuevo correo";
        }
    }
}