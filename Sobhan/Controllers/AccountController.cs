using DataLayer.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Sobhan.Common;
using Sobhan.DomainClasses;
using Sobhan.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sobhan.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class AccountController : Controller
    {
        private readonly IUsersUserIdentityServiceJwt _usersService;
        private readonly ITokenStoreService _tokenStoreService;
        private readonly IUnitOfWork _uow;
        private readonly IAntiForgeryCookieService _antiforgery;

        public AccountController(
            IUsersUserIdentityServiceJwt usersService,
            ITokenStoreService tokenStoreService,
            IUnitOfWork uow,
            IAntiForgeryCookieService antiforgery)
        {
            _usersService = usersService;
            _usersService.CheckArgumentIsNull(nameof(usersService));

            _tokenStoreService = tokenStoreService;
            _tokenStoreService.CheckArgumentIsNull(nameof(tokenStoreService));

            _uow = uow;
            _uow.CheckArgumentIsNull(nameof(_uow));

            _antiforgery = antiforgery;
            _antiforgery.CheckArgumentIsNull(nameof(antiforgery));
        }

        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody]  User loginUser)
        {
            if (loginUser == null)
            {
                return BadRequest("user is not set.");
            }

            var user = await _usersService.FindUserAsync(loginUser.Username, loginUser.Password);
            if (user == null || !user.IsActive)
            {
                return Unauthorized();
            }

            var (accessToken, refreshToken, claims) = await _tokenStoreService.CreateJwtTokens(user, refreshTokenSource: null);

            _antiforgery.RegenerateAntiForgeryCookies(claims);

            return Ok(new { access_token = accessToken, refresh_token = refreshToken });
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken([FromBody]JToken jsonBody)
        {
            var refreshToken = jsonBody.Value<string>("refreshToken");
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return BadRequest("refreshToken is not set.");
            }

            var token = await _tokenStoreService.FindTokenAsync(refreshToken);
            if (token == null)
            {
                return Unauthorized();
            }

            var (accessToken, newRefreshToken, claims) = await _tokenStoreService.CreateJwtTokens(token.UserIdentity, refreshToken);

            _antiforgery.RegenerateAntiForgeryCookies(claims);

            return Ok(new { access_token = accessToken, refresh_token = newRefreshToken });
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<bool> Logout(string refreshToken)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userIdValue = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;

            // The Jwt implementation does not support "revoke OAuth token" (logout) by design.
            // Delete the user's tokens from the database (revoke its bearer token)
            await _tokenStoreService.RevokeUserBearerTokensAsync(userIdValue, refreshToken);
            await _uow.SaveChangesAsync();

            _antiforgery.DeleteAntiForgeryCookies();

            return true;
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public IActionResult GetUserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            return Json(new { Username = claimsIdentity.Name });
        }
    }
}