using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interfaces
{
    public interface IUserRepositoryService : IRepositoryService<User>
    {
        //userName is UserName or Email
        Task<User> FindUserAsync(string userName, string password);
        Task<string> GetUserIdAsync(string userName, string password);

        Task<User> FindUserByUsername(string userName);

        Task<bool> LoginOrEmailIsAllreadyInUserAsync(string userName, string email);

        Task<User> GetWithRoleAsync(string userId);
    }
}
