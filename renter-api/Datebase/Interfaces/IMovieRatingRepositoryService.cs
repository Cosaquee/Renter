using Models.Models;

namespace Database.Interfaces
{
    public interface IMovieRatingRepositoryService : IRepositoryService<MovieRating>
    {
        float GetRate(long movieId);

        void RateMovie(long movieId, string userId, int rate);
    }
}