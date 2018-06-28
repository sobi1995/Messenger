using AutoMapper;
using DataLayer.Context;
using DomainClasses.Entity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Services.Hub;
using Services.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;
using ViewModel.Entitys.Chat;
using ViewModel.Entitys.User;

namespace Services.Services
{
    public class ChatService : BaseService<Chat>, IChatService
    {
        private readonly DbSet<Contacts> _Contacts;
     
        private readonly IUserService _user;
        private readonly IMapper _mapper;
         
        public ChatService(IUnitOfWork uow, IMapper mapper, IUserService user) : base(uow)
        {
            _Contacts = uow.Set<Contacts>();
          _mapper = mapper;
            _user = user;
        }

        public UserChat chatContact(int contactid, int my)
        {

            return (from c in _Contacts
                    where (c.User1Id == my && c.User2Id == contactid) || (c.User2Id == my && c.User1Id == contactid)
                    join chat in _TEntity on c.Id equals chat.ContactsId into cc
                    select new UserChat()
                    {
                        id = c.Id,
                        dialog = cc.Where(x => x.ContactsId == c.Id).Select(x => new ChatList() { id=x.Id,read=x.Read, message = x.Message, time = DateTime.Now, who = x.UserSendId }).ToList()
                    }

                   ).FirstOrDefault();
        }

        public IQueryable<ChatVm> GetChatQuery(Expression<Func<Chat, bool>> query)
        {
            return _TEntity.Where(query).Select(x => new ChatVm()
            {
                ContactsId = x.ContactsId,
                Message = x.Message,
                Read = x.Read,
              id=x.Id,
                UserReceiveId = x.UserReceiveId,
                UserSendId = x.UserSendId
            });
        }

        public int SaveMessage(SendMessage SendMessage)
        {
         //!!!!!!!!!!!!dbcontext must be inject
            dbContext db = new dbContext();
            //var chat  = _mapper.Map<SendMessage, Chat>(SendMessage);

          var  chat=new Chat()
            {
                ContactsId = SendMessage.sessionchat,
                Message = SendMessage.message,
                Read = 0,
                UserSendId = SendMessage.users,
                UserReceiveId = SendMessage.userr
            };
            db.Chat.Add(chat);
            db.SaveChanges();
            return chat.Id;
           
        }

        public int SeenMesage(int chatid)
        {
            var chatsseen = _TEntity.Find(chatid);
            chatsseen.Read = 1;
            _uow.MarkAsChanged(chatsseen);
            return _uow.SaveAllChanges();
        }
    }
}