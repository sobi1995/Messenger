using System.ComponentModel;

namespace Utility
{
    public enum Status
    {
        [Description("online")]
        Online = 1,
        [Description("offline")]
        Offline = 0
    }
}