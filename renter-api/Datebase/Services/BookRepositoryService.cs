using Database.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Database.Services
{
    public class BookRepositoryService : RepositoryService<Book>, IBookRepositoryService
    {
        public BookRepositoryService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
