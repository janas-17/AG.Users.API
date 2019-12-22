using AG.Users.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AG.Users.Data.Services
{
    /// <summary>
    /// Validation checks specifically for Operators
    /// </summary>
    public class OperatorValidationService
    {
        /// <summary>
        /// Assumed that an operator can be approved and update other data fields within the same operation
        /// Otherwise could have made a comparison hash built from other data fields and used that to determine
        /// that just the Approved data field was changed, and then allow subsequent updates post that operation
        /// </summary>
        /// <param name="operatorToValidate"></param>
        /// <returns></returns>
        public bool operatorApproved(Operator operatorToValidate)
        {
            if (operatorToValidate == null)
                throw new ArgumentNullException(nameof(operatorToValidate));

            if (!operatorToValidate.Approved)
                throw new Exception("Operator cannot be updated until Approved");

            return operatorToValidate.Approved;
        }
    }
}
