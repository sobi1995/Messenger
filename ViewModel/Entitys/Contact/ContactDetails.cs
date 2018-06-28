using System;

namespace ViewModel.Entitys.Contact
{
    public class ContactDetails
    {
        public int contactId { get; set; }
        public int id { get; set; }
        public DateTime lastMessageTime { get; set; }
        public string name { get; set; }

        public int unread { get; set; }
    }
}