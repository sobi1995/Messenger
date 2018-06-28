using System.Collections.Generic;

namespace ViewModel.SignalR
{
    public class UserConnectionSignalR
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }
}