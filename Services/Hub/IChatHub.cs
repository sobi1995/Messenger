using System.Collections.Generic;

namespace Services.Hub
{
    public interface IChatHub
    {
        HashSet<string> GetConnection(string username);
    }
}