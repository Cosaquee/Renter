using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Database.Interfaces;

namespace Database.Services
{
    public class UserRepositoryService : RepositoryService<User>, IUserRepositoryService
    {
        public UserRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
