using Sobhan.DomainClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClasses.Entity
{
  public  class UserImg:BaseEntity
    {
        public virtual User User { get; set; }
        public   int UserId { get; set; }
        public string Img { get; set; }
    }
}
