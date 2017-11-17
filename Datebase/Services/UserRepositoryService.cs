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

        public Task<User> FindUserAsync(string userName, string password)
        {
            var hashedPassword = PasswordHasher.CalculateHashedPassword(password);
            return Queryable().Include(x=>x.Role).Where(x => string.Equals(x.UserName, userName, StringComparison.InvariantCultureIgnoreCase) &&
                                                             string.Equals(x.Password, hashedPassword)).FirstOrDefaultAsync();
        }

        public Task<User> GetWithRoleAsync(string userId)
        {
            return Queryable().Include(x=>x.Role).Where(x => x.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<string> GetUserIdAsync(string userName, string password)
        {
            return (await Queryable().Where(x => string.Equals(x.UserName, userName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync())?.Id;
        }

        public async Task<bool> LoginOrEmailIsAllreadyInUserAsync(string userName, string email)
        {
            return (await Queryable().Where(x => string.Equals(x.UserName, userName, StringComparison.InvariantCultureIgnoreCase) ||
            string.Equals(x.Email, email, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync()) != null;
        }
    }
}
