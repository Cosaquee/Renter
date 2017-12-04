using Database.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Database.Services
{
    public class AuthorRepositoryService : RepositoryService<Author>, IAuthorRepositoryService
    {
        public AuthorRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
