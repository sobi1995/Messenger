namespace DomainClasses.Entity
{
    public class Chat : BaseEntity
    {
        public int UserSendId { get; set; }

        public int UserReceiveId { get; set; }
        public virtual Contacts Contacts { get; set; }
        public int ContactsId { get; set; }

        public string Message { get; set; }
        public int Read { get; set; }
    }
}