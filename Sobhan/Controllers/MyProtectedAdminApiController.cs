using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sobhan.Common;
using Sobhan.Services;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sobhan.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    //[Authorize(Policy = CustomRoles.User)]
    public class MyProtectedAdminApiController : Controller
    {
        private readonly IUsersUserIdentityServiceJwt _usersService;

        public MyProtectedAdminApiController(IUsersUserIdentityServiceJwt usersService)
        {
            _usersService = usersService;
            _usersService.CheckArgumentIsNull(nameof(usersService));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userDataClaim = claimsIdentity.FindFirst(ClaimTypes.UserData);
            var userId = userDataClaim.Value;

            return Ok(new
            {
                Username = this.User.Identity.Name,
                UserData = userId,
                TokenSerialNumber = await _usersService.GetSerialNumberAsync(int.Parse(userId)),
                Roles = claimsIdentity.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList()
            });
        }
    }
}