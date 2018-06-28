namespace Sobhan.DomainClasses
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual User UserIdentity { get; set; }
        public virtual Role Role { get; set; }
    }
}