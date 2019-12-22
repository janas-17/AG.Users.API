using AG.Users.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AG.Users.Data.Services
{
    /// <summary>
    /// Basic validation checks on any class inheriting from IUser
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class UserValidationService<TEntity>
        where TEntity : class, IUser
    {
        /// <summary>
        /// Assumed that the 200 max char requirement was for both the first name and last name combined
        /// Checks that the char length of names passed does not exceed 200 characters
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public bool ValidNameLength(string firstName, string lastName)
        {
            if ((firstName + lastName).Length > 200)
                throw new Exception("First Name and Last Name cannot be greater than 200 characters.");

            return true;
        }

        /// <summary>
        /// Ensures that the date of birth provided equates to an age between 18 and 120
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public bool ValidAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            // Compare date of current year with that of birth year
            // if  birth year greater deduct age to cater for year difference
            if (dateOfBirth.Date > today.AddYears(-age))
                age--;

            if (age < 18 || age > 120)
                throw new Exception("Date of Birth outside of acceptable range.");

            return true;
        }
    }
}
