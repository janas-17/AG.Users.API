using System;
using System.Collections.Generic;
using System.Text;

namespace AG.Users.EFCore.Models
{
    public class Operator : AUser
    {
        public string GameName { get; set; }

        /// <summary>
        /// Since this was defined as a bit in nature (pending or approved), set this as a bool (bit in DB)
        /// Possible improvement would be defining this as an enum with a possible char value in DB
        /// should the requirement of an additional Approved 'Status' arise
        /// </summary>
        public bool Approved { get; set; }
    }
}
