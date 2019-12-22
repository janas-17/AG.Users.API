using AG.Users.Data;
using AG.Users.EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AG.Users.API.Controllers
{
    /// <summary>
    /// A Generic Controller that uses a Generic Repository to provide 
    /// basic CRUD commands for any class inheriting from IUser
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    [Route("api/[controller]")]
    [ApiController]
    public abstract class AController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IUser
        where TRepository : IRepo<TEntity>
    {
        protected readonly TRepository repository;

        public AController(TRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var user = await repository.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get(string name)
        {
            return await repository.GetByName(name);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntity user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            await repository.Update(user);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity user)
        {
            await repository.Add(user);
            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var user = await repository.Delete(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

    }
}
