﻿using Database.Interfaces;
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

        public RentMovie Rent(long movieId, string userId, TimeSpan time)
        {
            var now = DateTime.Now;

            var rentBook = new RentMovie
            {
                MovieId = movieId,
                UserId = userId,
                From = now,
                To = now.Add(time)
            };

            this.Insert(rentBook);

            return rentBook;
        }
    }
}