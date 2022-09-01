using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

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

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {

        }
    }
}
