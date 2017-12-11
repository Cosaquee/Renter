using AutoMapper;
using Models.Dtos.Book;
using Models.Dtos.Role;
using Models.Dtos.User;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.AutoMapperProfiles
{
    public class DtosProfile : Profile
    {
        public DtosProfile()
        {
            BookMapings();
            UserMappings();
            RoleMappings();
        }

        private void BookMapings()
        {
            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();
        }

        private void UserMappings()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User, CreateUserDto>();
        }

        public void RoleMappings()
        {
            CreateMap<CreateRoleDto, Role>();
            CreateMap<Role, CreateRoleDto>();
        }
    }
}
