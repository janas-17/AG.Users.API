using AG.Users.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AG.Users.Data.Services
{
    public class UserValidationService<TEntity>
        where TEntity : class, IUser
    {
        public bool ValidNameLength(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }
        public bool ValidAge(DateTime dateOfBirth)
        {
            throw new NotImplementedException();
        }
    }
}
