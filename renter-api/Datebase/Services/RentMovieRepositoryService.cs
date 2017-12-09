using Database.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Database.Services
{
    public class RentMovieRepositoryService : RepositoryService<RentMovie>, IRentMovieRepositoryService
	{
		public RentMovieRepositoryService(DbContext dbContext) : base(dbContext)
		{
		}
	}
}
