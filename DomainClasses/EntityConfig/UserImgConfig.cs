using DomainClasses.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DomainClasses.EntityConfig
{
  public  class UserImgConfig: IEntityTypeConfiguration<UserImg>
    {
        public void Configure(EntityTypeBuilder<UserImg> builder)
        {
            builder.HasOne(x => x.User).
                 WithMany(x => x.UserImg).
                 HasForeignKey(x => x.UserId).
                 OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
