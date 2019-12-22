@@ -0,0 +1,51 @@
﻿using AG.Users.EFCore;
using AG.Users.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AG.Users.Tests
{
    /// <summary>
    /// Used to seed in memory DbContext with test data
    /// </summary>
    public static class DatabaseSeeder
    {
        public static void SeedTestData(UsersContext dbContext)
        {
            dbContext.Set<Operator>().Add(new Operator()
            {
                Approved = false,
                DateOfBirth = new DateTime(2000, 1, 1),
                FirstName = "John",
                LastName = "Doe",
                GameName = "Jdoe",
            });
            dbContext.Set<Operator>().Add(new Operator()
            {
                Approved = true,
                DateOfBirth = new DateTime(1990, 6, 6),
                FirstName = "Jane",
                LastName = "Doe",
                GameName = "jane",
            });
            dbContext.Set<Operator>().Add(new Operator()
            {
                Approved = true,
                DateOfBirth = new DateTime(1966, 6, 6),
                FirstName = "toDelete",
                LastName = "toDelete",
                GameName = "toDelete",
            });

            dbContext.Set<Administrator>().Add(new Administrator()
            {
                DateOfBirth = new DateTime(2000, 1, 1),
                FirstName = "Adam",
                LastName = "Smith",
            });

            dbContext.SaveChanges();
        }
    }
}