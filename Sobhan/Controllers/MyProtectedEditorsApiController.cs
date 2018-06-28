using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sobhan.Services;

namespace Sobhan.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = CustomRoles.Editor)]
    public class MyProtectedEditorsApiController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Id = 1,
                Title = "Hello from My Protected Editors Controller! [Authorize(Policy = CustomRoles.Editor)]",
                Username = this.User.Identity.Name
            });
        }
    }
}