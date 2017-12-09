using Database.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Database.Services
{
    public class MovieRepositoryService : RepositoryService<Movie>, IMovieRepositoryService
	{
		public MovieRepositoryService(DbContext dbContext) : base(dbContext)
		{
		}
	}
}
