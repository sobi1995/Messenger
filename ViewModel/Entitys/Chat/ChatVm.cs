namespace ViewModel.Entitys.Chat
{
    public class ChatVm
    {
        public int id { get; set; }
        public int UserSendId { get; set; }
        public int UserReceiveId { get; set; }
        public int ContactsId { get; set; }
        public string Message { get; set; }
        public int Read { get; set; }
    }
}