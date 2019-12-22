using System;
using System.Collections.Generic;
using System.Text;

namespace AG.Users.EFCore.Models
{
    public class Operator : AUser
    {
        public string GameName { get; set; }
        public bool Approved { get; set; }
    }
}
