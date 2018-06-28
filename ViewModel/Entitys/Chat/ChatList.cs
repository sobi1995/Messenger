using System;

namespace ViewModel.Entitys.Chat
{
    public class ChatList
    {
        public int id { get; set; }
        public string message { get; set; }
        public int read { get; set; }
        public int who { get; set; }
        public DateTime time { get; set; }
    }
}