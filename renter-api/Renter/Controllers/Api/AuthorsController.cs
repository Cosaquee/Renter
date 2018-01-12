using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Database.Interfaces;
using Models.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Renter.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/author")]
    public class AuthorsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAuthorRepositoryService authorRepositoryService;

        public AuthorsController(IUnitOfWork unitOfWork, IAuthorRepositoryService authorRepositoryService)
        {
            this.unitOfWork = unitOfWork;
            this.authorRepositoryService = authorRepositoryService;
        }

        // GET api/values
        [HttpGet]
        [Authorize(Roles = "Administrator, Employee, User")]
        public IEnumerable<Author> Get()
        {
            return authorRepositoryService.Queryable().Include(x => x.Books);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator, Employee, User")]
        public Author Get(long id)
        {
            return authorRepositoryService.Queryable().Where(x => x.Id == id).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        [Authorize(Roles = "Administrator, Employee")]
        public Author CreateAuthor([FromBody]Author author)
        {
            authorRepositoryService.Insert(author);
            unitOfWork.Save();

            return author;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, Employee")]
        public void UpdateAuthor(long id, [FromBody]Author author)
        {
            author.Id = id;
            authorRepositoryService.Update(author);
            unitOfWork.Save();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator, Employee")]
        public void DeleteAuthor(long id)
        {
            authorRepositoryService.Delete(id);
            unitOfWork.Save();
        }

        [HttpGet("Books/{authorId}")]
        [Authorize(Roles = "Administrator, Employee, User")]
        public IActionResult GetBooks(long authorId)
        {
            var books = authorRepositoryService.Queryable().Include(x => x.Books).Select(x => x.Books).ToList();
            return Ok(books);
        }
    }
}
