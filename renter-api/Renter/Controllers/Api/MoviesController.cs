﻿using AutoMapper;
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

        public MoviesController(IUnitOfWork unitOfWork, IMovieRepositoryService movieRepositoryService,
                                IRentMovieRepositoryService rentMovieRepositoryService, IMovieRatingRepositoryService movieRatingRepositoryService)
        {
            this.unitOfWork = unitOfWork;
            this.movieRepositoryService = movieRepositoryService;
            this.rentMovieRepositoryService = rentMovieRepositoryService;
            this.movieRatingRepositoryService = movieRatingRepositoryService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return movieRepositoryService.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Movie Get(long id)
        {
            return movieRepositoryService.Queryable().Where(x => x.Id == id).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public void CreateMovie([FromBody]EditMovieDto movie)
        {
            movieRepositoryService.Insert(Mapper.Map<Movie>(movie));
            unitOfWork.Save();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void UpdateMovie(long id, [FromBody]EditMovieDto movie)
        {
            var movieDb = Mapper.Map<Movie>(movie);
            movieDb.Id = id;
            movieRepositoryService.Update(movieDb);
            unitOfWork.Save();
        }

        [HttpGet("Rent/{movieId}/{userId}/{days}")]
        public IActionResult Rent(long movieId, string userId, int days)
        {
            var timeSpan = TimeSpan.FromDays(days);
            var rentMovie = rentMovieRepositoryService.Rent(movieId, userId, timeSpan);
            if (rentMovie == null)
            {
                return BadRequest("Movie is not avaiable for rent.");
            }
            return Ok(rentMovie);
        }

        [HttpGet("RentHistory/{movieId}")]
        public IActionResult RentHistory(long movieId)
        {
            var rentMovie = rentMovieRepositoryService.GetMovieRentHisotry(movieId);
            return Ok(rentMovie);
        }

        [HttpGet("Rate/{movieId}")]
        public IActionResult Rate(long movieId)
        {
            var rate = this.movieRatingRepositoryService.GetRate(movieId);
            return Ok(rate);
        }

        [HttpPost("Rate/{movieId}/{userId}/{rate}")]
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
        public IActionResult GetMoviesByCategory(string categoryName)
        {
            var movies = this.movieRepositoryService.Queryable().Include(x => x.Category).Where(x => string.Equals(x.Category.Name, categoryName, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(movies);
        }
    }
}