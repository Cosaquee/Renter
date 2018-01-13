using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Database.Services
{
    public class DirectorRepositoryService : RepositoryService<Director>, IDirectorRepositoryService
    {
        public DirectorRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}