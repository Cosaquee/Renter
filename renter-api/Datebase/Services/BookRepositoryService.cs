using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Database.Services
{
    public class BookRepositoryService : RepositoryService<Book>, IBookRepositoryService
    {
        public BookRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}