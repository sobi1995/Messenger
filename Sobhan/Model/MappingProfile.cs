using AutoMapper;
using DomainClasses.Entity;
using Sobhan.DomainClasses;
using ViewModel.Entitys.Chat;
using ViewModel.Entitys.User;

namespace Sobhan.Model
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, UserRegister>();
            CreateMap<UserRegister, User>();

            CreateMap<Chat, SendMessage>()
                  .ForMember(x => x.message, m => m.MapFrom(src => src.Message))
                    .ForMember(x => x.sessionchat, m => m.MapFrom(src => src.ContactsId))
                      .ForMember(x => x.userr, m => m.MapFrom(src => src.UserReceiveId))
                        .ForMember(x => x.users, m => m.MapFrom(src => src.UserSendId));
            CreateMap<SendMessage, Chat>()
                       .ForMember(x => x.Message, m => m.MapFrom(src => src.message))
                        .ForMember(x => x.ContactsId, m => m.MapFrom(src => src.sessionchat))
                         .ForMember(x => x.UserReceiveId, m => m.MapFrom(src => src.userr))
                          .ForMember(x => x.UserSendId, m => m.MapFrom(src => src.users));
        }
    }
}