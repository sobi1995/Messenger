using DomainClasses.Entity;
using System;
using System.Collections.Generic;
using Utility;

namespace Sobhan.DomainClasses
{
    public class User : BaseEntity
    {
        public User()
        {
            UserRoles = new List<UserRole>();
            UserTokens = new List<UserToken>();
            this.contacts1 = new List<Contacts>();
            this.contacts2 = new List<Contacts>();
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Mobile { get; set; }
        public string Bio { get; set; }
        //public string avatar { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Status Status { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? LastLoggedIn { get; set; }

        /// <summary>
        /// every time the user changes his Password,
        /// or an admin changes his Roles or stat/IsActive,
        /// create a new `SerialNumber` GUID and store it in the DB.
        /// </summary>
        public string SerialNumber { get; set; }

        public virtual List<UserRole> UserRoles { get; set; }
        public virtual List<UserToken> UserTokens { get; set; }
        public virtual List<Contacts> contacts1 { get; set; }
        public virtual List<Contacts> contacts2 { get; set; }
        public virtual List<UserImg> UserImg { get; set; }

        
    }
}