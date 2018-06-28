using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Sobhan.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [Authorize]
    public class MyProtectedApiController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Id = 1,
                Username = this.User.Identity.Name
            });
        }
    }
}