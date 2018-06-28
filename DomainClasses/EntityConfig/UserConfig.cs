using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sobhan.DomainClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClasses.EntityConfig
{
   public class UserConfig: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {


        }
    }
}
