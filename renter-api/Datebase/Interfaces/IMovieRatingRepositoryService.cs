using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Interfaces
{
    public interface IMovieRatingRepositoryService : IRepositoryService<MovieRating>
    {
        float GetRate(int movieId);
        void RateMovie(int movieId, string userId, int rate);
    }
}
