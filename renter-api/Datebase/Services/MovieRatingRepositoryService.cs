using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Linq;

namespace Database.Services
{
    public class MovieRatingRepositoryService : RepositoryService<MovieRating>, IMovieRatingRepositoryService
    {
        public MovieRatingRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }

        public float GetRate(long movieId)
        {
            var rates = Queryable().Where(x => x.MovieId == movieId);
            var ratesCount = rates.Count();

            if (ratesCount <= 0)
                return 0;

            float sumRates = (float)rates.Sum(x => x.Rate);
            return sumRates / ratesCount;
        }

        public MovieRating GetRateByUser(long movieId, string userId)
        {
            return Queryable().Where(x => x.MovieId == movieId && x.UserId == userId).FirstOrDefault();
        }

        public void RateMovie(long movieId, string userId, int rate)
        {
            if (this.IsRateValid(rate))
                throw new Exception("Invalid rate");

            var rateItem = GetRateByUser(movieId, userId);
            if (rateItem != null)
            {
                rateItem.Rate = rate;
                this.Update(rateItem);
            }
            else
            {
                rateItem = new MovieRating
                {
                    MovieId = movieId,
                    UserId = userId,
                    Rate = rate
                };
                this.Insert(rateItem);
            }
        }

        private const int minRate = 1;
        private const int maxRate = 10;

        private bool IsRateValid(int rate)
        {
            return rate >= minRate && rate <= maxRate;
        }
    }
}