using System.Collections.Generic;
using ViewModel.Entitys.Chat;

namespace ViewModel.Entitys.User
{
    public class UserChat
    {
        public int id { get; set; }
        public int my { get; set; }
        public int contact { get; set; }
        public int unread { get; set; }
     
        public List<ChatList> dialog { get; set; }
    }
}