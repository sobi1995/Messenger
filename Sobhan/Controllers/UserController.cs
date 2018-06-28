using DataLayer.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Sobhan.Services;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Utility;
using ViewModel.Entitys.User;

namespace Sobhan.Controllers
{
    //[Authorize(Policy = CustomRoles.User)]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userservice;
        private readonly IUserImg _userimg;
        public UserController(IUserService userservice, IUserImg userimg)
        {
            _userservice = userservice;
            _userimg = userimg;
        }

        [HttpPost("Create")]
        [AllowAnonymous]
        public IActionResult Create([FromBody]UserRegister UserRegister)
        {

             



            return Ok(_userservice.Create(UserRegister));
        }

        [HttpGet("GetChatList")]
        public IActionResult GetChatList(int userid)
        {
            return Ok(_userservice.GetChatList(userid));
        }

        [HttpGet("getUser")]
        public IActionResult getUser(int userid)
        {
            return Ok(_userservice.getUser(userid));
        }
        //HGJHG
        [HttpPost]
        [Route("uploadsystemuserprofilephoto")]
        public IActionResult UploadSystemUserProfilePhoto(int userid)
        {
            string filename = "";
            string path = "";
            var files = HttpContext.Request.Form.Files;
            long size = 0;
            foreach (var file in files)
            {
                path = "wwwroot/images/";
                filename  = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                path += filename;
                   size += file.Length;
                using (FileStream fs = System.IO.File.Create(path))
                {
                    
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }


            _userimg.AddImg(filename, userid);

            return Ok(filename);
        }

        [HttpGet("getUserContact")]
        public IActionResult getUserContact(int userid)
        {
            return Ok(_userservice.GetUserContact(userid));
        }



        [HttpPost("setStatus")]
        public IActionResult setStatus(Status status,int userid)
        {
            return Ok(_userservice.SetStatus(status,userid));
        }
        
    }
}