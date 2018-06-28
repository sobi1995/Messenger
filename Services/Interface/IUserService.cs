using Sobhan.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;
using ViewModel.Entitys;
using ViewModel.Entitys.Contact;
using ViewModel.Entitys.User;
using ViewModel.Temp;

namespace Services.Interface
{
    public interface IUserService
    {
        int Create(UserRegister UserRegister);

        List<UserChat> GetChatList(int userid);

        ChatProfile getUser(int userid);

        IQueryable<UserVm> Where(System.Linq.Expressions.Expression<Func<User, bool>> query);
        ContactList GetUserContact(int contactid);
        int SetStatus(Status status,int userid);
    }
}