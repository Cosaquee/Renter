using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Database.Services
{
    public class RoleRepositoryService : RepositoryService<Role>, IRoleRepositoryService
    {
        public RoleRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}