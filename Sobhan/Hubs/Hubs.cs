//using Sobhan.Services;

//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.SignalR;
//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Sobhan.Hubs
//{
//    [Authorize(Policy = CustomRoles.Admin, AuthenticationSchemes = " Bearer")]
//    public class ChatHubtest : Hub<ITypedHubClient>
//    {nvbfbnvbnv
//        private static readonly ConcurrentDictionary<string, UserSocket> Users = new ConcurrentDictionary<string, UserSocket>();
//        private static object _lock = new object();
//        public void SendToAll(string name, string message)
//        {
//            var userName = Context.User.Identity.Name;
//            Clients.All.InvokeAsync("sendToAll", name, message);
//        }

//        public override Task OnConnectedAsync()
//        {
//            var userName = Context.User.Identity.Name;
//            var connectionId = Context.ConnectionId;

//            var user = Users.GetOrAdd(userName,
//                _ => new UserSocket
//                {
//                    Name = userName,
//                    ConnectionIds = new HashSet<string>()
//                });
//            lock (user.ConnectionIds)
//            {
//                user.ConnectionIds.Add(connectionId);
//            }
//            return base.OnConnectedAsync();
//        }

//        public override Task OnDisconnectedAsync(Exception exception)
//        {
//            return base.OnDisconnectedAsync(exception);
//        }
//    }
//}

//public class UserSocket
//{
//    //public int Id { set; get; }
//    public string Name { get; set; }
//    // سایر خواص کاربر

//    public HashSet<string> ConnectionIds { get; set; }
//}