using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientInfoController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            Response.Cookies.Append("server_cookie", "Hello-from-Server",
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddMinutes(30),
                    Domain = Request.Host.Host,
                    Path = "/",
                    IsEssential = true
                }
            );

            return Ok();
        }
    }
}
