using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services.Utils;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Database.Services
{
    public class UserRepositoryService : RepositoryService<User>, IUserRepositoryService
    {
        public UserRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }

        public User LoginByEmail(string email, string password)
        {
            var hashedPassword = PasswordHasher.CalculateHashedPassword(password);
            return Queryable().Include(x => x.Role).Where(x => x.Email == email && x.Password == hashedPassword).FirstOrDefault();
        }
        
        public User FindUserByEmail(string email)
        {
            return Queryable().Where(x => x.Email == email).FirstOrDefault();
        }

        public User GetWithRole(string userId)
        {
            return Queryable().Include(x => x.Role).Where(x => x.Id == userId).FirstOrDefault();
        }

        public bool LoginOrEmailIsAllreadyInUser(string email) => Queryable().Where(x => x.Email == email).Any();
        public List<User> FetchAllUsers() => Queryable().Include(x => x.Role).Include(x => x.RentBooks).Include(x => x.RentBooks).ToList();
    }
}