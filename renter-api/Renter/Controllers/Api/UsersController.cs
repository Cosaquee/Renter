using Authorization;
using Database.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models.Dtos.Login;
using Models.Dtos.User;
using Models.Models;
using Services.UserServices.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Models.Dtos.UserCover;

namespace Renter.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepositoryService userRepositoryService;
        private readonly ITokenProvider tokenProvider;
        private readonly IUserManagementService userManagementService;
        private readonly IRentBookRepositoryService rentBookRepositoryService;


        public UsersController(IUnitOfWork unitOfWork, IUserRepositoryService userRepositoryService, ITokenProvider tokenProvider, IUserManagementService userManagementService, IRentBookRepositoryService rentBookRepositoryService)
        {
            this.unitOfWork = unitOfWork;
            this.userRepositoryService = userRepositoryService;
            this.tokenProvider = tokenProvider;
            this.userManagementService = userManagementService;
            this.rentBookRepositoryService = rentBookRepositoryService;
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
        [HttpPost("authorize")]
        [ProducesResponseType(typeof(AuthToken), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Authorize([FromBody]LoginDto loginDto)
        {
            var token = tokenProvider.GenerateToken(loginDto.Username, loginDto.Password);
            var user = userRepositoryService.FindUserByEmail(loginDto.Username);
            if (token == null)
                return NotFound("Bad username or password");
            // TODO: Create better way to handle adding user to token
            token.User = user;

            return Json(token);
        }

        /// <response code="200">Returns auth token</response>
        /// <response code="404">Refresh token does not exist or expired.</response>
        [HttpPost("authorize/refresh")]
        [ProducesResponseType(typeof(AuthToken), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult RefreshToken(string refreshToken)
        {
            var token = tokenProvider.RefreshToken(refreshToken);
            if (token == null)
                return NotFound("Refresh token does not exist or expired.");
            return Json(token);
        }

        // GET api/values
        //[HttpGet]
        //public IEnumerable<User> Get()
        //{
        //    return userRepositoryService.Get();
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public User Get(string id)
        {
            return userRepositoryService.Queryable().Where(x => x.Equals(id)).FirstOrDefault();
        }

        /// <remarks>
        /// "Authorization": "Bearer {accessToken}"
        /// example
        /// {
        ///     "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiNzI3ZjRhOGYtYWFhYi00YzMyLWFmNmQtZGM0Yzc5NWY3NzUwLTYzNjQ2NDc0Njc2NjQ1NDk2Mi10ZXN0IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiUmVnaXN0cmVkIiwianRpIjoiZTI4ZDUyNzItZWE2ZS00NDljLThiODEtNzRjZTcxZTllYTUwLTE1MTA5MTk5NDAiLCJpYXQiOjE1MTA5MTk5NDAsIm5iZiI6MTUxMDkxNjM0MCwiZXhwIjoxNTExMDAyNzQwLCJpc3MiOiJDTVNSZW50YWwiLCJhdWQiOiJDTVNSZW50YWwifQ.Nmhegxy71oTkZ98z1z50YueP8IRpQObAhHEsXZWeUyQ"
        /// }
        /// </remarks>
        /// <response code="200">Authorized</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Authentication was provided, but the authenticated user is not permitted to perform the requested operation.</response>
        [HttpGet("test")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Test()
        {
            return Json(User.Identity);
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
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status412PreconditionFailed)]
        public IActionResult CreateUser([FromBody]CreateUserDto createUserDto)
        {
            // Validate Model
            if (!ModelState.IsValid)
            {
                Response.StatusCode = StatusCodes.Status412PreconditionFailed;
                return Json(ModelState);
            }

            // Check if user already exits
            var alreadyExists = userRepositoryService.LoginOrEmailIsAllreadyInUser(createUserDto.Email);
            if (alreadyExists)
            {
                Response.StatusCode = StatusCodes.Status409Conflict;
                return Json("User name or email is already in use.");
            }

            // Prepare user creation model
            var userCreationResult = userManagementService.CreateUser(createUserDto);
            if (userCreationResult.User == null)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return Json(userCreationResult.Errors);
            }

            // add user to db
            userRepositoryService.Insert(userCreationResult.User);
            unitOfWork.Save();

            return StatusCode(201);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void UpdateUser(string id, [FromBody]User user)
        {
            user.Id = id;
            userRepositoryService.Update(user);
            unitOfWork.Save();
        }

        [HttpPost("avatar")]
        [Authorize(Roles="Administrator, Employee")]
        public void AddAvatar([FromBody] UserCover userCover)
        {
            var user = userRepositoryService.Get(userCover.UserID);
            user.ProvileAvatar = userCover.AvatarUrl;
            userRepositoryService.Update(user);
            unitOfWork.Save();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void DeleteUser(string id)
        {
            userRepositoryService.Delete(id);
            unitOfWork.Save();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IEnumerable<User> Index() => this.userRepositoryService.FetchAllUsers();
    }
}