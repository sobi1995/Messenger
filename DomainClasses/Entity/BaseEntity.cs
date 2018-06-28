using System;

namespace DomainClasses.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public BaseEntity()
        {
            this.DateTime = DateTime.Now;
        }
    }
}