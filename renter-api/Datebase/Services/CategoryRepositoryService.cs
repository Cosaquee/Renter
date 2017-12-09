using Database.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Database.Services
{
    public class CategoryRepositoryService : RepositoryService<Category>, ICategoryRepositoryService
    {
		public CategoryRepositoryService(DbContext dbContext) : base(dbContext)
        {
		}
    }
}
