using System.Collections.Generic;
using ViewModel.Entitys;
using ViewModel.Entitys.Contact;

namespace Services.Interface
{
    public interface IContactService
    {
        List<ContactList> GetUserContacts(int userid);

        ContactDetails GetContact(int my, int contactid);

        bool IsContact(int contactid, int my);

        int AddContact(CreateContact contact);

        void SendContactSignal(AddContact AddContact, string username1, string username2);
    }
}