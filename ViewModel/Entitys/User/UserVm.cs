using Utility;

namespace ViewModel.Entitys.User
{
    public class UserVm
    {
       public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Mobile { get; set; }
        public string Bio { get; set; }
        public string avatar { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Status Status { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
    }
}