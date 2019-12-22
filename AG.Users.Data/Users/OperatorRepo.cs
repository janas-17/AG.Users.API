using AG.Users.Data.Services;
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
        protected readonly OperatorValidationService operatorValidation;
        public OperatorRepo(UsersContext context, UserValidationService<Operator> userValidation,
            OperatorValidationService operatorValidation) : base(context, userValidation)
        {
            this.operatorValidation = operatorValidation;
        }

        protected override bool saveValidationChecksSuccess(Operator entity)
        {
            bool validationSuccessful = base.saveValidationChecksSuccess(entity);

            // If basic checks pass and this is an update (Id is not 0), do additional operator check
            if (validationSuccessful && entity.Id != 0)
                validationSuccessful = operatorValidation.operatorApproved(entity);

            return validationSuccessful;
        }
    }
}
