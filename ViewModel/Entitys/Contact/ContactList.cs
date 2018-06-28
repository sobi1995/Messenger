using Common;
using Utility;
using Sobhan.Common;
namespace ViewModel.Entitys
{
    public class ContactList
    {//sdfsddfsfsd
        public int id { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
        public string mood { get; set; }
        public Status statusenum { get; set; }
        public string status =>  statusenum.GetDescription();
 

    }


}

