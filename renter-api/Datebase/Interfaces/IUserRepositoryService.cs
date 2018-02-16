using System.Collections.Generic;
using Models.Models;

namespace Database.Interfaces
{
    public interface IUserRepositoryService : IRepositoryService<User>
    {
        User LoginByEmail(string email, string password);

        User FindUserByEmail(string email);

        bool LoginOrEmailIsAllreadyInUser(string email);

        User GetWithRole(string userId);
        List<User> FetchAllUsers();
    }
}