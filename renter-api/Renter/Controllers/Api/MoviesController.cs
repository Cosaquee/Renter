using AutoMapper;
using Database.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Dtos.Movie;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Renter.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/movie")]
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMovieRepositoryService movieRepositoryService;
        private readonly IRentMovieRepositoryService rentMovieRepositoryService;
        private readonly IMovieRatingRepositoryService movieRatingRepositoryService;

        public MoviesController(IUnitOfWork unitOfWork, IMovieRepositoryService movieRepositoryService, IRentMovieRepositoryService rentMovieRepositoryService, IMovieRatingRepositoryService movieRatingRepositoryService)
        {
            this.unitOfWork = unitOfWork;
            this.movieRepositoryService = movieRepositoryService;
            this.rentMovieRepositoryService = rentMovieRepositoryService;
            this.movieRatingRepositoryService = movieRatingRepositoryService;
        }

        // GET api/values
        [HttpGet]
        [Authorize(Roles="Administrator, Employee, User")]
        public IEnumerable<Movie> Get()
        {
            return movieRepositoryService.Queryable().Include(x => x.Category).Include(x => x.Director);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize(Roles="Administrator, Employee, User")]
        public Movie Get(long id)
        {
            return movieRepositoryService.Queryable().Where(x => x.Id == id).Include(x => x.Category).Include(x => x.Director).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        [Authorize(Roles="Administrator, Employee")]
        public void CreateMovie([FromBody]EditMovieDto movie)
        {
            var newMovie = new Movie
            {
                Title = movie.Title,
                CategoryId = movie.CategoryId,
                DirectorId = movie.DirectorId,
                Duration = TimeSpan.FromMinutes(movie.Duration),
                Description = movie.Description,
            };

            movieRepositoryService.Insert(newMovie);
            unitOfWork.Save();
        }

        [HttpPost("cover")]
        [Authorize(Roles="Administrator, Employee")]
        public void AddCover([FromBody] MovieCoverDTO movieCover)
        {
            var movie = movieRepositoryService.Queryable().Where(x => x.Id == movieCover.MovieID).FirstOrDefault();
            movie.CoverURL = movieCover.CoverURL;
            movieRepositoryService.Update(movie);
            unitOfWork.Save();
        }

        [HttpPost("rent/{movieId}/{userId}/{quality}")]
        [Authorize(Roles="Administrator, Employee, User")]
        public IActionResult Rent(long movieId, string userId, MovieQuality quality)
        {
            var timeSpan = TimeSpan.FromDays(10);
            var rentMovie = rentMovieRepositoryService.Rent(movieId, userId, timeSpan, quality);
            if (rentMovie == null)
            {
                return BadRequest("Movie is not avaiable for rent.");
            }
            return Ok(rentMovie);
        }

        [HttpGet("RentHistory/{movieId}")]
        [Authorize(Roles="Administrator, Employee, User")]
        public IActionResult RentHistory(long movieId)
        {
            var rentMovie = rentMovieRepositoryService.GetMovieRentHisotry(movieId);
            return Ok(rentMovie);
        }

        [HttpGet("Rate/{movieId}")]
        [Authorize(Roles="Administrator, Employee, User")]
        public IActionResult Rate(long movieId)
        {
            var rate = this.movieRatingRepositoryService.GetRate(movieId);
            return Ok(rate);
        }

        [HttpPost("Rate/{movieId}/{userId}/{rate}")]
        [Authorize(Roles="Administrator, Employee, User")]
        public IActionResult Rate(long movieId, string userId, int rate)
        {
            try
            {
                this.movieRatingRepositoryService.RateMovie(movieId, userId, rate);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Category/{categoryName}")]
        [Authorize(Roles="Administrator, Employee, User")]
        public IActionResult GetMoviesByCategory(string categoryName)
        {
            var movies = this.movieRepositoryService.Queryable().Include(x => x.Category).Where(x => string.Equals(x.Category.Name, categoryName, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(movies);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var book = this.movieRepositoryService.Get(id);
            this.movieRepositoryService.Delete(book);
            return Ok("Movie deleted");
        }

        [HttpGet("latest")]
        [Authorize(Roles = "Administrator, Employee, User")]
        public IEnumerable<Movie> Latest() => this.movieRepositoryService.Queryable().Include(x => x.Director).Include(x => x.Category).Distinct().Take(5);

        [HttpGet("rent/current/{userID}")]
        [Authorize(Roles = "Administrator, Employee, User")]
        public IEnumerable<RentMovie> GetCurrentRentedMovies(string userID) => this.rentMovieRepositoryService.GetCurrentRentedMovies(userID);

        [HttpGet("history/user/{userID}")]
        [Authorize(Roles = "Administrator, Employee, User")]
        public IEnumerable<RentMovie> GetUserRentHistory(string userID) => this.rentMovieRepositoryService.GetUserMovieRentHistory(userID);

        [HttpGet("history/movie/{movieID}")]
        [Authorize(Roles = "Administrator, Employee, User")]
        public IEnumerable<RentMovie> GetMovieRentHistory(long movieID) => this.rentMovieRepositoryService.GetMovieRentHistory(movieID);

        [HttpGet("rented/{movieID}/{userID}")]
        [Authorize(Roles = "Administrator, Employee, User")]
        public bool IsCurrentlyRented(long movieID, string userID) => this.rentMovieRepositoryService.IsCurrentlyRented(movieID, userID);

        [HttpGet("rent")]
        [Authorize(Roles = "Administrator, Employee, User")]
        public IEnumerable<RentMovie> AllRentedMovies() => this.rentMovieRepositoryService.GetAllRentedMovies(); 
    }
}
