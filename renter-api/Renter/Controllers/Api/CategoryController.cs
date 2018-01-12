using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Database.Interfaces;
using Models.Models;
using System.Collections.Concurrent;

namespace Renter.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Category")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICategoryRepositoryService categoryRepositoryService;

        public CategoryController(IUnitOfWork unitOfWork, ICategoryRepositoryService categoryRepositoryService)
        {
            this.unitOfWork = unitOfWork;
            this.categoryRepositoryService = categoryRepositoryService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Employee, User")]
        public IEnumerable<Category> Get()
        {
           
            return categoryRepositoryService.Get().ToList();
           
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Category Get(long id)
        {
            return categoryRepositoryService.Queryable().Where(x => x.Id == id).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public void CreateCategory([FromBody]Category category)
        {
            categoryRepositoryService.Insert(category);
            unitOfWork.Save();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void UpdateCategory(long id, [FromBody]Category category)
        {
            category.Id = id;
            categoryRepositoryService.Update(category);
            unitOfWork.Save();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void DeleteCategory(long id)
        {
            categoryRepositoryService.Delete(id);
            unitOfWork.Save();
        }

    }
}