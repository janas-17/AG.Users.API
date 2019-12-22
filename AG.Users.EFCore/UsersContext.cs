using AG.Users.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AG.Users.EFCore
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        { }

        /// <summary>
        /// I assumed that given the common factors Operators and Administrators could be stored
        /// within a common structure, using a discriminator to differentiate between types
        /// 
        /// Possible improvement would be defining discriminator as enum with char db value
        /// should the requirement of an additional User Type arise
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AUser>()
                .ToTable("Users")
                .HasDiscriminator<int>("UserType")
                .HasValue<Operator>(1)
                .HasValue<Administrator>(2);

            modelBuilder.Entity<AUser>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<AUser>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            // I assumed the Max Length here given the requirement of 200 characters for FirstName + LastName
            modelBuilder.Entity<AUser>()
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            // I assumed the Max Length here given the requirement of 200 characters for FirstName + LastName
            modelBuilder.Entity<AUser>()
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
