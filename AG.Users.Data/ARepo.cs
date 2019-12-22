using AG.Users.Data.Services;
using AG.Users.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.Users.Data
{
    /// <summary>
    /// A Generic Repository capable of basic CRUDcommands for any class inheriting from IUser
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public abstract class ARepo<TEntity, TContext> : IRepo<TEntity>
        where TEntity : class, IUser
        where TContext : DbContext
    {
        protected readonly TContext context;
        protected readonly UserValidationService<TEntity> userValidation;

        public ARepo(TContext context, UserValidationService<TEntity> userValidation)
        {
            this.context = context;
            this.userValidation = userValidation;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            if (saveValidationChecksSuccess(entity))
            {
                context.Set<TEntity>().Add(entity);
                await context.SaveChangesAsync();
            }

            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Get(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// I assumed that the lookup described wanted to filter in both standard User Names
        /// Also assumed that character case is ignored
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByName(string name)
        {
            return await context
                .Set<TEntity>()
                .Where(x => (x.FirstName + x.LastName).ToLower().Contains(name.ToLower()))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            if (saveValidationChecksSuccess(entity))
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }

            return entity;
        }

        /// <summary>
        /// Validation checks that must occur for any user to save (insert or update)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool saveValidationChecksSuccess(TEntity entity)
        {
            bool validationSuccessful = userValidation.ValidNameLength(entity.FirstName, entity.LastName);

            // If valid post previous check, performa additional validation
            if (validationSuccessful)
                validationSuccessful = userValidation.ValidAge(entity.DateOfBirth);

            return validationSuccessful;
        }

    }
}
