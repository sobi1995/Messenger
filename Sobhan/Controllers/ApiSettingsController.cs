using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sobhan.Common;
using Sobhan.WebApp.Models;

namespace Sobhan.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class ApiSettingsController : Controller
    {//sdfsdfbcvbcvbcvbcvb
        private readonly IOptionsSnapshot<ApiSettings> _apiSettingsConfig;

        public ApiSettingsController(IOptionsSnapshot<ApiSettings> apiSettingsConfig)
        {
            _apiSettingsConfig = apiSettingsConfig;
            _apiSettingsConfig.CheckArgumentIsNull(nameof(apiSettingsConfig));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_apiSettingsConfig.Value); // For the Angular Client
        }
    }
}