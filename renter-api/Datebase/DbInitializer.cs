using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DbContext dbContext;

        public DbInitializer(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbSet<Role> roles;
        private DbSet<User> users;

        public async Task Seed()
        {
            roles = dbContext.Set<Role>();
            users = dbContext.Set<User>();

            await SeedRoles();
            await SeedUsers();
        }

        private async Task SeedRoles()
        {
            if(roles == null || await roles.AnyAsync())
            {
                return;
            }

            roles.Add(new Role { Name = "User" });
            roles.Add(new Role { Name = "Moderator" });
            roles.Add(new Role { Name = "Administrator" });

            dbContext.SaveChanges();
        }

        private async Task SeedUsers()
        {
            if (users == null || await users.AnyAsync())
            {
                return;
            }

            var userRole = await roles.FirstOrDefaultAsync(x => x.Name == "User");
            var moderatorRole = await roles.FirstOrDefaultAsync(x => x.Name == "Moderator");
            var administratorRole = await roles.FirstOrDefaultAsync(x => x.Name == "Administrator");

            users.Add(new User
            {
                Role = userRole,
                UserName = "testUser",
                Password = PasswordHasher.CalculateHashedPassword("testUser"),
                Email = "testUser@tt.tt",

            });

            users.Add(new User
            {
                Role = moderatorRole,
                UserName = "testModerator",
                Password = PasswordHasher.CalculateHashedPassword("testModerator"),
                Email = "testModerator@tt.tt",

            });

            users.Add(new User
            {
                Role = administratorRole,
                UserName = "testAdministrator",
                Password = PasswordHasher.CalculateHashedPassword("testAdministrator"),
                Email = "testAdministrator@tt.tt",

            });

            dbContext.SaveChanges();

        }

    }
}
