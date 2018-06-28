using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Entitys.Chat;
using ViewModel.Entitys.User;

namespace ViewModel.Entitys.Contact
{
  public  class AddContact
    {
      
        public List<ContactList> contact { get; set; }
        public int chatid { get; set; }
        public int msgid { get; set; }
    }
}
