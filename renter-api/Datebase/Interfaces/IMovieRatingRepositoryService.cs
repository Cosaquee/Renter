using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Interfaces
{
    public interface IMovieRatingRepositoryService : IRepositoryService<MovieRating>
    {
        float GetRate(long movieId);
        void RateMovie(long movieId, string userId, int rate);
    }
}
