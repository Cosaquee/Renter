using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Database.Services
{
    public class MovieRepositoryService : RepositoryService<Movie>, IMovieRepositoryService
    {
        public MovieRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}