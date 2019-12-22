using AG.Users.EFCore;
using AG.Users.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.Users.Data.Users
{
    public class OperatorRepo : ARepo<Operator, UsersContext>
    {
        public OperatorRepo(UsersContext context) 
            : base(context)
        {

        }
    }
}
