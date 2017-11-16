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
using Authorization;
using Services.UserServices.Interfaces;
using Models.Dtos.User;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HelloWorld.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepositoryService userRepositoryService;
        private readonly ITokenProvider tokenProvider;
        private readonly IUserManagementService userManagementService;

        public UsersController(IUnitOfWork unitOfWork, IUserRepositoryService userRepositoryService, ITokenProvider tokenProvider, IUserManagementService userManagementService)
        {
            this.unitOfWork = unitOfWork;
            this.userRepositoryService = userRepositoryService;
            this.tokenProvider = tokenProvider;
            this.userManagementService = userManagementService;
        }

        //[HttpPost("/authorize")]        
        /// <summary>
        /// Returns auth token
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="password">password.</param>
        /// <returns></returns>
        /// <response code="200">Returns auth token</response>
        /// <response code="404">Bad username or password</response>      
        [HttpPost("/authorize")]
        [ProducesResponseType(typeof(AuthToken), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Authorize(string userName, string password)
        {
            var token = await tokenProvider.GenerateToken(userName, password);
            if (token == null)
                return NotFound("Bad username or password");
            return Json(token);
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return userRepositoryService.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(string id)
        {
            return userRepositoryService.Queryable().Where(x => x.Equals(id)).FirstOrDefault();
        }

        // POST api/values        
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="createUserDto">The create user dto.</param>
        /// <returns></returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If creation fails</response>       
        /// <response code="409">If user name or email is allready taken</response>
        /// <response code="412">If model validation fails</response>
        [HttpPost]

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserDto createUserDto)
        {
            //Validate Model
            if(!ModelState.IsValid)
            {
                Response.StatusCode = StatusCodes.Status412PreconditionFailed;
                return Json(ModelState);
            }

            //Check if user allready exits
            var allreadyExists = await userRepositoryService.LoginOrEmailIsAllreadyInUserAsync(createUserDto.UserName, createUserDto.Email);
            if(allreadyExists)
            {
                Response.StatusCode = StatusCodes.Status409Conflict;
                return Json("User name or email is allready in use.");
            }

            //Prepare user creation model
            var userCreationResult = userManagementService.CreateUser(createUserDto);
            if (userCreationResult.User == null)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return Json(userCreationResult.Errors);
            }

            //add user to db
            userRepositoryService.Insert(userCreationResult.User);
            await unitOfWork.SaveAsync();

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void UpdateUser(string id, [FromBody]User user)
        {
            user.Id = id;
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