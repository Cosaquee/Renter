using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Database.Services
{
    public class CategoryRepositoryService : RepositoryService<Category>, ICategoryRepositoryService
    {
        public CategoryRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}