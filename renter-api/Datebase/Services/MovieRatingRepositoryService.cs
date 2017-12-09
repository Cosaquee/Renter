using Database.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Database.Services
{
    public class MovieRatingRepositoryService : RepositoryService<MovieRating>, IMovieRatingRepositoryService
	{
		public MovieRatingRepositoryService(DbContext dbContext) : base(dbContext)
		{
		}
	}
}
