using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Database.Interfaces;
using Models.Models;
using System.Security.Cryptography;
using System.Text;

namespace HelloWorld.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepositoryService userRepositoryService;

        public UsersController(IUnitOfWork unitOfWork, IUserRepositoryService userRepositoryService)
        {
            this.unitOfWork = unitOfWork;
            this.userRepositoryService = userRepositoryService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return userRepositoryService.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return userRepositoryService.Queryable().Where(x => x.Id == id).FirstOrDefault();
        }

        private const string _salt = "P&0myWHq";
        private static string CalculateHashedPassword(string clearpwd)
        {
            using (var sha = SHA256.Create())
            {
                var computedHash = sha.ComputeHash(Encoding.Unicode.GetBytes(clearpwd + _salt));
                return Convert.ToBase64String(computedHash);
            }
        }
        // POST api/values
        [HttpPost]
        public void CreateUser([FromBody]User user)
        {
            user.Password = CalculateHashedPassword(user.Password);
            userRepositoryService.Insert(user);
            unitOfWork.Save();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void UpdateUser(int id, [FromBody]User user)
        {
            user.Id = id;
            user.Password = CalculateHashedPassword(user.Password);
            userRepositoryService.Update(user);
            unitOfWork.Save();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
            userRepositoryService.Delete(id);
            unitOfWork.Save();
        }
    }
}