using Database.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Database.Services
{
    public class RoleRepositoryService : RepositoryService<Role>, IRoleRepositoryService
    {
        public RoleRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
