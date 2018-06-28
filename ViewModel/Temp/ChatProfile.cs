using Sobhan.Common;
using System.Collections.Generic;
using Utility;

namespace ViewModel.Temp
{
    public class ChatProfile
    {
        public string avatar { get; set; }
        //public List<chatList> chatList { get; set; }
        public int id { get; set; }
        public string mood { get; set; }
        public string name { get; set; }
        public Status statusenum { get; set; }
        public string status => statusenum.GetDescription();
    }
}

public class chatList
{
    public int contactId { get; set; }
    public int id { get; set; }
    public string lastMessageTime { get; set; }
    public string name { get; set; }
    public int unread { get; set; }
}