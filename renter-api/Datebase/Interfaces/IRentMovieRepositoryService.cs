using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Interfaces
{
    public interface IRentMovieRepositoryService : IRepositoryService<RentMovie>
    {
        List<RentMovie> GetMovieRentHisotry(string userId);
        List<RentMovie> GetMovieRentHisotry(long movieId);
        RentMovie Rent(long movieId, string userId, TimeSpan time);
    }
}
