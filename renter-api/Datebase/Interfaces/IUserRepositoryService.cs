﻿using Models.Models;

namespace Database.Interfaces
{
    public interface IUserRepositoryService : IRepositoryService<User>
    {
        //userName is UserName or Email
        User FindUser(string userName, string password);

        string GetUserId(string userName, string password);

        User FindUserByUsername(string userName);

        bool LoginOrEmailIsAllreadyInUser(string userName, string email);

        User GetWithRole(string userId);
    }
}