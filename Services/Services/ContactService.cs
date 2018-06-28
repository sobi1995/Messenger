using DataLayer.Context;
using DomainClasses.Entity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Services.Hub;
using Services.Interface;
using Sobhan.DomainClasses;
using Sobhan.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModel.Entitys;
using ViewModel.Entitys.Contact;

namespace Services.Services
{
    public class ContactService : BaseService<Contacts>, IContactService
    {
        private readonly DbSet<User> _user;
        private readonly DbSet<Chat> _chat;
        private readonly IUserService _UserService;
        private readonly DbSet<UserImg> _userimg;
        private readonly IChatHub _IChatHub ;
        
        private readonly IHubContext<ChatHub> _messageHubContext;
        public ContactService(IUnitOfWork uow, IUserService UserService, IHubContext<ChatHub> messageHubContext, IChatHub IChatHub) : base(uow)
        {
            _user = uow.Set<User>();
            _userimg= uow.Set<UserImg>();
            _chat = uow.Set<Chat>();
            _UserService = UserService;
            _messageHubContext = messageHubContext;
            _IChatHub = IChatHub;
        }

        public int AddContact(CreateContact contact)
        {
            var useridphone = _UserService.Where(x => x.Mobile == contact.phone).Select(x=> new ContactList() {
                firstname=x.FirstName,
                id=x.Id,
                lastname=x.LastName,
                mood=x.Bio,
                statusenum=x.Status,
                username=x.Username
            }).FirstOrDefault();
            var my = _UserService.Where(x => x.Id == contact.my).Select(x => new ContactList()
            {
                firstname = x.FirstName,
                id = x.Id,
                lastname = x.LastName,
                mood = x.Bio,
                statusenum = x.Status,
                username = x.Username
            }).FirstOrDefault();

            if (useridphone == null)
                return 2; //Phone Not Found
            else if (IsContact(useridphone.id, contact.my))
                return 3; // Contact Is Exsist
            else
            {
 
               var cont=new Contacts()
                {
                    NickName = contact.nickname,
                    Notes = contact.notes,
                    User1Id = contact.my,
                    User2Id = useridphone.id,
                    Chat= new List<Chat> { new Chat() { Message = DateTime.Now.ToString(), Read = 0, UserReceiveId = useridphone.id,UserSendId=my.id } }
                };

                _TEntity.Add(cont);
                _uow.SaveAllChanges();
                var AddContact = new AddContact() {msgid=cont.Chat.First().Id, chatid = cont.Id, contact = new List<ContactList>() { useridphone, my } };

                SendContactSignal(AddContact, useridphone.username, my.username);
                return 1;
            }
        }

        public void SendContactSignal(AddContact AddContact, string username1,string username2) {

            //!!!باید قسمت اخر با فیرست نوشته شود  فقط قسمت لیست ،لیست باشد


            var con1 = _IChatHub.GetConnection(username1);
            var con2= _IChatHub.GetConnection(username2);
              if(con1!=null)
            foreach (var item in con1)
                _messageHubContext.Clients.Client(item.ToString()).InvokeAsync("AddContactSignal", AddContact);
            if (con2 != null)
                foreach (var item in con2)
                _messageHubContext.Clients.Client(item.ToString()).InvokeAsync("AddContactSignal", AddContact);
           

        }

        public ContactDetails GetContact(int contactid, int my)
        {
            return (from c in _TEntity
                    where (c.User1Id == my && c.User2Id == contactid) || (c.User1Id == contactid && c.User2Id == my)
                    from u in _user
                    where u.Id == contactid
                    select new ContactDetails()
                    {
                        contactId = contactid,
                        id = c.Id,
                        name = u.DisplayName,
                        unread = 4,
                        lastMessageTime = DateTime.Now,
                    }
                     ).FirstOrDefault();
        }

        public List<ContactList> GetUserContacts(int userid)
        {//The Imageuser must joined
            return (from M in _TEntity
                    where M.User1Id == userid
                       || M.User2Id == userid
                    let ContactId = M.User1Id == userid ? M.User2Id : M.User1Id
                    join u in _user on ContactId equals u.Id

                    select new ContactList
                    {
                        avatar = _userimg.Where(x=> x.UserId==u.Id).Select(x=> x.Img).FirstOrDefault(),
                        firstname = M.User1Id == userid ? u.FirstName : u.FirstName,
                        id = M.User1Id == userid ? u.Id : u.Id,
                        lastname = M.User1Id == userid ? u.LastName : u.LastName,
                        mood = M.User1Id == userid ? u.Bio : u.Bio,
                        statusenum = u.Status,
                        username = M.User1Id == userid ? u.Username : u.Username,
                    })
                           .Distinct().ToList();
        }

        public bool IsContact(int contactid, int my)
        {
            return _TEntity.Any(x => (x.User2Id == contactid && x.User1Id == my) || (x.User2Id == my && x.User1Id == contactid)); ;
        }
    }
}