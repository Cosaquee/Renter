using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Database.Services
{
    public class AuthorRepositoryService : RepositoryService<Author>, IAuthorRepositoryService
    {
        public AuthorRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}