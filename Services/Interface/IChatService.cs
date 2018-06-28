using DomainClasses.Entity;
using System;
using ViewModel.Entitys.Chat;
using ViewModel.Entitys.User;

namespace Services.Interface
{
    public interface IChatService
    {
        UserChat chatContact(int contactid, int my);

        int SaveMessage(SendMessage SendMessage);

        System.Linq.IQueryable<ChatVm> GetChatQuery(System.Linq.Expressions.Expression<Func<Chat, bool>> query);
        int SeenMesage(int chatid);
    }
}