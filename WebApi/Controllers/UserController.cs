using AutoMapper;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.DTOs.UserDtos;
using WebApi.Errors;
using static WebApi.DTOs.UserDtos.UpdateUserDto;

namespace WebApi.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserManager<UserEntities> _userManager;
        private readonly SignInManager<UserEntities> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher<UserEntities> _passwordHasher;
        private readonly IMapper _mapper;

        public UserController(UserManager<UserEntities> userManager,
                              SignInManager<UserEntities> signInManager,
                              ITokenService tokenService,
                              IPasswordHasher<UserEntities> passwordHasher,
                              IMapper mapper)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseUserDto>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new CodeErrorResponse(401, "El correo ingresado no existe"));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new CodeErrorResponse(401, "La contraseña ingresada es incorrecta"));
            }

            var responseUserDto = _mapper.Map<ResponseUserDto>(user);

            responseUserDto.Token = _tokenService.CreateToken(user);

            return responseUserDto;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ResponseUserDto>> Register([FromBody] RegistrationDto registrationDto)
        {
            var user = _mapper.Map<UserEntities>(registrationDto);
            
            var result = await _userManager.CreateAsync(user, registrationDto.Password);

            if (!result.Succeeded || user == null)
            {
                return BadRequest(new CodeErrorResponse(400, "Algo salió mal. Por favor intentalo nuevamente"));
            }
            
            var responseUserDto = _mapper.Map<ResponseUserDto>(user);

            responseUserDto.Token = _tokenService.CreateToken(user);

            return responseUserDto;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ResponseUserDto>> GetUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return NotFound(new CodeErrorResponse(401, "El usuario no existe"));

            var responseUserDto = _mapper.Map<ResponseUserDto>(user);

            return responseUserDto;
        }

        [Authorize]
        [HttpPut("newPassword")]
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
        [HttpPut("newEmail")]
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

            return "Correo electronico actualizado. En tu proximo inicio de sesión ingresa tu nuevo correo";
        }
    }
}