using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Sobhan.Services;
using ViewModel.Entitys.Chat;

namespace Sobhan.Controllers
{
    [Produces("application/json")]
    [Route("api/Caht")]
    //[Authorize(Policy = CustomRoles.User)]
    public class CahtController : Controller
    {
        private IChatService _ChatService;

        public CahtController(IChatService _chhatservice)
        {
            this._ChatService = _chhatservice;
        }

        [HttpGet("chatContact")]
        public IActionResult chatContact(int contactid, int my)
        {
            return Ok(_ChatService.chatContact(contactid, my));
        }

        [HttpPost("SendMessage")]
        public IActionResult SendMessage([FromBody] SendMessage SendMessage)
        {
            return Ok(_ChatService.SaveMessage(SendMessage));
        }
    }
}