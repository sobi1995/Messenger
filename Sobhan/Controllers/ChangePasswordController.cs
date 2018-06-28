using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sobhan.Common;
using Sobhan.Services;
using Sobhan.WebApp.Models;
using System.Threading.Tasks;

namespace Sobhan.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class ChangePasswordController : Controller
    {
        private readonly IUsersUserIdentityServiceJwt _usersService;

        public ChangePasswordController(IUsersUserIdentityServiceJwt usersService)
        {
            _usersService = usersService;
            _usersService.CheckArgumentIsNull(nameof(usersService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody]ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _usersService.GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest("NotFound");
            }

            var result = await _usersService.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Error);
        }
    }
}