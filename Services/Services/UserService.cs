using AutoMapper;
using DataLayer.Context;
using DomainClasses.Entity;
using Microsoft.EntityFrameworkCore;
using Services.Hub;
using Services.Interface;
using Sobhan.DomainClasses;
using Sobhan.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Utility;
using ViewModel.Entitys;
using ViewModel.Entitys.Chat;
using ViewModel.Entitys.Contact;
using ViewModel.Entitys.User;
using ViewModel.Temp;

namespace Services.Services
{
    public class UserService : BaseService<User>, IUserService, IUserImg
    {
        private readonly DbSet<Chat> _Chat;
        private readonly DbSet<UserImg> _UserImg;
        private readonly DbSet<Contacts> _Contacts;
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;

        public UserService(
            IUnitOfWork uow,
            IMapper mapper,
            ISecurityService securityService
             ) : base(uow)
        {
            _mapper = mapper;
            _securityService = securityService;
            _Chat = _uow.Set<Chat>();
            _Contacts = _uow.Set<Contacts>();
            _UserImg = _uow.Set<UserImg>();
           
        }

        // Assign the object in the constructor for dependency injection

        public int Create(UserRegister UserRegister)
        {


            dbContext db = new dbContext();



            //!!!!!! Must do by one MarkAsAdd
             
            if (_TEntity.Any(x => x.Mobile == UserRegister.Mobile))
                return 2;
            var usercreate = _mapper.Map<UserRegister, User>(UserRegister);
            usercreate.Password = _securityService.GetSha256Hash(usercreate.Password);
            usercreate.IsActive = true;
            usercreate.Username = UserRegister.FirstName.Substring(0, 2)+ UserRegister.Mobile.Substring(0,5);
            usercreate.SerialNumber = Guid.NewGuid().ToString("N");
            usercreate.DisplayName = UserRegister.LastName;
            //usercreate.contacts1.Add(new Contacts() { User2Id = 1, Chat = new List<Chat>() { new Chat() {UserReceiveId=1,Message=DateTime.Now.ToString(),Read=0 } } });
            usercreate.contacts1.Add(new Contacts() { User2Id = 1  });
            usercreate.UserRoles.Add(new UserRole() { RoleId = 2 });
            db.Users.Add(usercreate);
           
              db.SaveAllChanges();
             db.Chat.Add(new Chat() {
                ContactsId = usercreate.contacts1.First().Id,
                Message = DateTime.Now.ToString(),
                UserReceiveId = usercreate.contacts1.First().User2Id ,
                UserSendId = usercreate.contacts1.First().User1Id,
                Read = 0
            });
            db.SaveAllChanges();
            return 1;
        }

        public List<UserChat> GetChatList(int userid)
        {
            return (from M in _Contacts
                    where M.User1Id == userid ||
                    M.User2Id == userid
                    join cat in _Chat on M.Id equals cat.ContactsId into g

                    select new UserChat()
                    {
                        my = userid,
                        unread = g.Where(x => x.ContactsId == M.Id && x.Read == 0).Count(),
                        id = g.Where(x=> x.ContactsId==M.Id).Select(x => x.ContactsId).FirstOrDefault(),
                        contact = M.User1Id == userid ? M.User2Id : M.User1Id,
                        dialog = g.Where(v => v.ContactsId == M.Id).Select(x => new ChatList() {id=x.Id,read=x.Read, message = x.Message, time = DateTime.Now, who = x.UserReceiveId }).ToList()
                    }).Where(x => x.id != 0).ToList();

            //var b = _Chat.Where(x => a.Contains(x.ContactsId)).Select(x => x.Message).ToList();
            //var b = (from C in _Chat
            //         where a.Contains(C.ContactsId)
            //         join u in _TEntity on C.UserReceiveId equals u.Id
            //         select new { u.Username }).ToList();
        }

        public ChatProfile getUser(int userid)
        {//!!!!!!!!!the imageuser must joined
            return (from u in _TEntity
                    where u.Id == userid               
            
                    select new ChatProfile()
                    {
                        avatar = _UserImg.Where(x => x.UserId == u.Id).Select(x => x.Img).FirstOrDefault(),
                        id = u.Id,
                        mood = u.Bio,
                        name = u.DisplayName,
                        statusenum =u.Status,
                       
                    }).FirstOrDefault();
        }

        public int AddImg(string img, int userid)
        {
            _UserImg.Add(new UserImg() { UserId = userid, Img = img });
            return _uow.SaveAllChanges();

        }

        public IQueryable<UserVm> Where(Expression<Func<User, bool>> query)
        {
            return _TEntity.Where(query).Select(x => new UserVm()
            {
                avatar = _UserImg.Where(ig => ig.UserId == x.Id).Select(ig => ig.Img).FirstOrDefault(),
                Bio = x.Bio,
                DisplayName = x.DisplayName,
                FirstName = x.FirstName,
                IsActive = x.IsActive,
                LastName = x.LastName,
                Mobile = x.Mobile,
                Password = x.Password,
                Status = x.Status,
                Username = x.Username,
                Id = x.Id
            });
        }

        public ContactList GetUserContact(int contactid)
        {
            return _TEntity.Where(x => x.Id == contactid).Select(x => new ContactList()
            {
                avatar = _UserImg.Where(ig => ig.UserId == x.Id).Select(ig => ig.Img).FirstOrDefault(),
                firstname = x.FirstName,
                id = x.Id,
                lastname = x.LastName,
                mood = x.Bio,
               statusenum=x.Status,
                username = x.Username
                
                
            }).FirstOrDefault();
        }
       public int SetStatus(Status status, int userid)
        {
           var user = _TEntity.Find(userid);
            user.Status = status;
            _uow.MarkAsChanged(user);
            return _uow.SaveAllChanges();
        }
    }
}