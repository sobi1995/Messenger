using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Sobhan.Services;
using ViewModel.Entitys.Contact;

namespace Sobhan.Controllers
{
    [Produces("application/json")]
    [Route("api/Contact")]
    //[Authorize(Policy = CustomRoles.User)]
    public class ContactController : Controller
    {
        #region Prop

        private readonly IContactService _ContactService;
        private readonly IUserService _UserService;

        #endregion Prop

        #region Ctor

        public ContactController(
            IContactService ContactService,
            IUserService UserService)
        {
            _ContactService = ContactService;
            _UserService = UserService;
        }

        #endregion Ctor

        [HttpGet]
        public IActionResult GetContacts(int userid)
        {
            return Ok(_ContactService.GetUserContacts(userid));
        }

        [HttpGet("GetContact")]
        public IActionResult GetContact(int userid, int my)
        {
            return Ok(_ContactService.GetContact(userid, my));
        }

        [HttpPost("AddContact")]
        public IActionResult AddContact([FromBody]CreateContact contact)
        {
            return Ok(_ContactService.AddContact(contact));
        }
    }
}