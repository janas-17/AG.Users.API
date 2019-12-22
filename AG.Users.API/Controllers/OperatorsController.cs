using AG.Users.Data.Users;
using AG.Users.EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AG.Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorsController : AController<Operator, OperatorRepo>
    {
        public OperatorsController(OperatorRepo repository) : base(repository)
        {

        }
    }
}
