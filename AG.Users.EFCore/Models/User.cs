using System;
using System.Collections.Generic;
using System.Text;

namespace AG.Users.EFCore.Models
{
    public abstract class AUser : IUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

    }

    public interface IUser
    {
        int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
