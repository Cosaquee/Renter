using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Services
{
    public class RentMovieRepositoryService : RepositoryService<RentMovie>, IRentMovieRepositoryService
    {
        public RentMovieRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }

        public List<RentMovie> GetMovieRentHisotry(string userId)
        {
            return Queryable().Where(x => x.UserId == userId).ToList();
        }

        public List<RentMovie> GetMovieRentHisotry(long movieId)
        {
            return Queryable().Where(x => x.MovieId == movieId).ToList();
        }

        public RentMovie Rent(long movieId, string userId, TimeSpan time, MovieQuality quality)
        {
            var now = DateTime.Now;

            var rentBook = new RentMovie
            {
                MovieId = movieId,
                UserId = userId,
                From = now,
                To = now.Add(time),
                MovieState = MovieState.ACTIVE,
                MovieQuality = quality
            };

            this.Insert(rentBook);
            dbContext.SaveChanges();

            return rentBook;
        }

        public List<RentMovie> GetUserMovieRentHistory(string userID)
        {
            return Queryable().Where(x => x.UserId == userID).Where(x => x.MovieState == MovieState.EXPIRED).Include(x => x.Movie).ToList();
        }

        public List<RentMovie> GetMovieRentHistory(long movieID)
        {
            return Queryable().Where(x => x.MovieId == movieID).Where(x => x.MovieState == MovieState.EXPIRED).ToList();
        }

        public List<RentMovie> GetCurrentRentedMovies(string userID)
        {
            return Queryable().Where(x => x.UserId == userID).Where(x => x.MovieState == MovieState.ACTIVE).Include(x => x.Movie).Include(x => x.User).ToList();
        }

        public List<RentMovie> GetAllRentedMovies() => Queryable().Include(x => x.User).Include(x => x.Movie).ToList();

        public bool IsCurrentlyRented(long movieID, string userID)
        {
            var movie = Queryable().Where(x => x.MovieId == movieID).Where(x => x.UserId == userID).Where(x => x.MovieState == MovieState.ACTIVE).FirstOrDefault();
            return !(movie == null);
        }
    }
}