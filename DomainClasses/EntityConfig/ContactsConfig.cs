using DomainClasses.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainClasses.EntityConfig
{
    public class ContactsConfig : IEntityTypeConfiguration<Contacts>
    {
        public void Configure(EntityTypeBuilder<Contacts> builder)
        {
            builder.HasOne(x => x.User1).
                WithMany(x => x.contacts1).
                HasForeignKey(x => x.User1Id).
                OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User2).
             WithMany(x => x.contacts2).
             HasForeignKey(x => x.User2Id).
             OnDelete(DeleteBehavior.Restrict);
        }
    }
}