using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("socialnetworksapi/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            var result = HttpContext.Response.StatusCode;

            SocialNetworks socialNetworks = new SocialNetworks(result);

            return socialNetworks.ToString();

            /*await HttpContext.Response.WriteAsync($"Status: {result}");*/
        }
    }
}
