using DomainClasses.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainClasses.EntityConfig
{
    public class ChatConfig : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasOne(x => x.Contacts).
                 WithMany(x => x.Chat).
                 HasForeignKey(x => x.ContactsId).
                       OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(x => x.UserSend).
            //    WithMany(x => x.UserSend).
            //    HasForeignKey(x => x.UserSendId).
            //          OnDelete(DeleteBehavior.Restrict);
        }
    }
}