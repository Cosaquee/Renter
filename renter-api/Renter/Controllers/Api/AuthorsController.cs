using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Database.Interfaces;
using Models.Models;
using AutoMapper;

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
        public IEnumerable<Author> Get()
        {
            return authorRepositoryService.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Author Get(int id)
        {
            return authorRepositoryService.Queryable().Where(x => x.Id == id).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public void CreateAuthor([FromBody]Author author)
        {
            authorRepositoryService.Insert(author);
            unitOfWork.Save();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void UpdateAuthor(int id, [FromBody]Author author)
        {
            author.Id = id;
            authorRepositoryService.Update(author);
            unitOfWork.Save();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void DeleteAuthor(int id)
        {
            authorRepositoryService.Delete(id);
            unitOfWork.Save();
        }
    }
}
