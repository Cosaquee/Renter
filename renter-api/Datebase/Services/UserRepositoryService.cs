using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Database.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Services.Utils;

namespace Database.Services
{
    public class UserRepositoryService : RepositoryService<User>, IUserRepositoryService
    {
        public UserRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }

        public User FindUserByUsername(string userName)
        {
            return Queryable().Where(x => x.UserName == userName).FirstOrDefault();
        }

        public User FindUser(string userName, string password)
        {
            var hashedPassword = PasswordHasher.CalculateHashedPassword(password);
            return Queryable().Include(x=>x.Role).Where(x => string.Equals(x.UserName, userName, StringComparison.InvariantCultureIgnoreCase) &&
                                                             string.Equals(x.Password, hashedPassword)).FirstOrDefault();
        }

        public User GetWithRole(string userId)
        {
            return Queryable().Include(x=>x.Role).Where(x => x.Id == userId).FirstOrDefault();
        }

        public string GetUserId(string userName, string password)
        {
            return Queryable().Where(x => string.Equals(x.UserName, userName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.Id;
        }

        public bool LoginOrEmailIsAllreadyInUser(string userName, string email)
        {
            return Queryable().Where(x => string.Equals(x.UserName, userName, StringComparison.InvariantCultureIgnoreCase) ||
            string.Equals(x.Email, email, StringComparison.InvariantCultureIgnoreCase)).Any();
        }
    }
}
