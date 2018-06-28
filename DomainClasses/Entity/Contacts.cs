using Sobhan.DomainClasses;
using System.Collections.Generic;

namespace DomainClasses.Entity
{
    public class Contacts : BaseEntity
    {
        public User User1 { get; set; }
        public int User1Id { get; set; }
        public virtual User User2 { get; set; }
        public int User2Id { get; set; }
        public ICollection<Chat> Chat { get; set; }
        public string NickName { get; set; }
        public string Notes { get; set; }
    }
}