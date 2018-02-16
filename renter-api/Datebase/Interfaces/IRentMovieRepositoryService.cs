using Models.Models;
using System;
using System.Collections.Generic;

namespace Database.Interfaces
{
    public interface IRentMovieRepositoryService : IRepositoryService<RentMovie>
    {
        List<RentMovie> GetMovieRentHisotry(string userId);

        List<RentMovie> GetMovieRentHisotry(long movieId);

        RentMovie Rent(long movieId, string userId, TimeSpan time, MovieQuality quality);

        List<RentMovie> GetUserMovieRentHistory(string userID);
        List<RentMovie> GetMovieRentHistory(long movieID);

        List<RentMovie> GetCurrentRentedMovies(string userID);

        bool IsCurrentlyRented(long movieID, string userID);
        List<RentMovie> GetAllRentedMovies();
    }
}