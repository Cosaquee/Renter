using AutoMapper;
using Models.Dtos.User;
using Models.Models;
using Services.UserServices.Interfaces;
using Services.UserServices.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.UserServices
{
    public class UserManagementService : IUserManagementService
    {
        private readonly int DefaultRoleId = 1;

        public UserCreationResult CreateUser(CreateUserDto createUserDto)
        {
            var user = Mapper.Map<User>(createUserDto);
            user.Password = Utils.PasswordHasher.CalculateHashedPassword(user.Password);

            var ticks = DateTime.Now.Ticks;
            var guid = Guid.NewGuid().ToString();
            user.Id = $"{guid}-{ticks}-{user.UserName}";
            user.RoleId = DefaultRoleId;

            return new UserCreationResult
            {
                User = user,
                Errors = null
            };
        }
    }
}
