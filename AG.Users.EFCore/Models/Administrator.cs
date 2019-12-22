using System;
using System.Collections.Generic;
using System.Text;

namespace AG.Users.EFCore.Models
{
    public class Administrator : AUser
    {
        public List<Operator> Operators { get; set; }
    }
}
