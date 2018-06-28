using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Services.Hub;
using Services.Interface;
using Sobhan.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using ViewModel.Entitys.Chat;
using ViewModel.SignalR;

namespace Sobhan.Hubs
{
    [Authorize(Policy = CustomRoles.User, AuthenticationSchemes = " Bearer")]
    public class ChatHub : Hub, IChatHub
    {
        private static readonly ConcurrentDictionary<string, UserSocket> Users = new ConcurrentDictionary<string, UserSocket>();
        private static object _lock = new object();
        private readonly IUserService _user;
        private readonly IChatService  _chat;
  

        public ChatHub(   IUserService  user, IChatService chat) 
        {
            _user = user;
            _chat = chat;
            
        }

        public void Send(SendMessage message)
        {//!!!هنگام چت با کاربر و ارسال پیغام ایدی چت را نداریم باید همزمان وفتی یک  سوکا برای کاربر ارسال میشود یک سوکت برای خودمان نیز ارسال شوود تا 
         // ایدی پیغام را داشته باشیم  روش دیگر ان این است ایدی را یا جیوایدی از کلاینت بفرستین
            message.read = 0;
            string usernamecontact = _user.Where(x => x.Id == message.userr).Select(x => x.Username).FirstOrDefault();

            var conationsids = GetConnection(usernamecontact);
            var my = GetConnection(Context.User.Identity.Name);
            message.id = _chat.SaveMessage(message);
            if (conationsids != null)  
              
            foreach (var conationsid in conationsids)
                {
                
                    Clients.Client(conationsid).InvokeAsync("Send", message);
                }
                //!!این قسمت باید اصلح شود
           
            if (my != null)
              
            foreach (var conationsid in my)
            {

                Clients.Client(conationsid).InvokeAsync("Send", message);
            }
        }

        public override Task OnConnectedAsync()
        {
            var userName = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName,
                _ => new UserSocket
                {
                    Name = userName,
                    ConnectionIds = new HashSet<string>()
                });
            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
            }
            return base.OnConnectedAsync();
        }

        //qehwegqwgehjqgejhqgej
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public HashSet<string> GetConnection(string username)
        {
            //!!!!!!!the must fix
            UserSocket Con = new UserSocket();
       
            if (Users.TryGetValue(username, out Con) == false)
                return null;
            else
                return Con.ConnectionIds;
                //return Users.GetValueOrDefault(username).ConnectionIds;
       
        }

        public void StatusUser(string[] usernamecontact,int my,Status status)
        {
          //!!!!
            foreach (var  contact in usernamecontact)
            {
                try
                {

               
                var contactConnections = GetConnection(contact);
                foreach (var Connections in contactConnections)
                {
                    Clients.Client(Connections).InvokeAsync("SignalStatus", new StatusMy() {userid=my,statusenum=status });
                }
                }
                catch (Exception)
                {

                    ;
                }
            }

            _user.SetStatus(status, my);

        }

        public void Seen(SeenMsg SeenMsg)
        {
            var conationsids = GetConnection(SeenMsg.usernamecontact);

            if (conationsids != null)
                foreach (var conationsid in conationsids)
                {
                    Clients.Client(conationsid).InvokeAsync("SeenMsg", SeenMsg);
                }
            _chat.SeenMesage(SeenMsg.msgid);


        }

        public void AddContact(int contactid,int my) {
     



        }
    }
}

public class UserSocket
{
    //public int Id { set; get; }
    public string Name { get; set; }

    // سایر خواص کاربر

    public HashSet<string> ConnectionIds { get; set; }
}