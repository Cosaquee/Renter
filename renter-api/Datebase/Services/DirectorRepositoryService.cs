using Database.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Database.Services
{
	public class DirectorRepositoryService : RepositoryService<Director>, IDirectorRepositoryService
	{
		public DirectorRepositoryService(DbContext dbContext) : base(dbContext)
		{
		}
	}
}
