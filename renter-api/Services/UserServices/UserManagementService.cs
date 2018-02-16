using AutoMapper;
using Models.Dtos.User;
using Models.Models;
using Services.UserServices.Interfaces;
using Services.UserServices.Results;
using System;

namespace Services.UserServices
{
    public class UserManagementService : IUserManagementService
    {
        private readonly int DefaultRoleId = 1;

        public UserCreationResult CreateUser(CreateUserDto createUserDto)
        {
            // var user = Mapper.Map<User>(createUserDto);
            var user = new User
            {
                Name = createUserDto.Name,
                Surname = createUserDto.Surname,
                Email = createUserDto.Email,
            };

            user.Password = Utils.PasswordHasher.CalculateHashedPassword(createUserDto.Password);

            var ticks = DateTime.Now.Ticks;
            var guid = Guid.NewGuid().ToString();
            user.Id = $"{guid}-{ticks}";
            user.RoleId = DefaultRoleId;

            return new UserCreationResult
            {
                User = user,
                Errors = null
            };
        }
    }
}